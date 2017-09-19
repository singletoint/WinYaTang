using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.IO;
using Jil;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using HtmlAgilityPack;
using System.Threading;

namespace WinYatang
{
    public partial class frm_Login : Form
    {
        public const string PageURL_Base = "https://jr.yatang.cn";
        public const string PageURL_Login = "https://yztapi.yatang.cn/loginM.jsp?origin=1&source=1&synUrl=https://jr.yatang.cn/index.php?s=/index/index/*type:1&findPwdUrl=https://jr.yatang.cn/index.php?s=/new_login/lookpassword/*type:3&regUrl=https://jr.yatang.cn/NewRegister/*type:2";

        public const string PageURL_LoginPost = "https://yztapi.yatang.cn/yluser";
        private const string PageURL_InvestDetail = "https://jr.yatang.cn/Invest/ViewBorrow/ibid/";
        private const string PageURL_GetUserCoupon = "https://jr.yatang.cn/Ajax/getUserCoupon";
        private const string PageURL_InvestCheckPPay = "https://jr.yatang.cn/Invest/checkppay";

        public const string PageURL_Index = "/index.php?s=/index/index";
        private const string cookiesDir = "Cookies";

        public frm_Login()
        {
            InitializeComponent();
        }
        private CookieContainer cookie = null;
        private Dictionary<string, CookieContainer> CookiesDict = null;

        private void frm_Login_Load(object sender, EventArgs e)
        {
            //var ppay = Encrypt("fmy712", "15f6c805ba51da28a6350de6a2720f12"); //支付密码
            //var pppay = Decrypt(ppay, "15f6c805ba51da28a6350de6a2720f12");
            //ppay = XXTEA.Code(, "15f6c805ba51da28a6350de6a2720f12", XXTEAMode.Encrypt);

            PnOptor.Hide();
            PnLogin.Show();
        }

        private void ReLoadCookieUsers()
        {
            CookiesDict = new Dictionary<string, CookieContainer>();
            cboSelectUser.Items.Clear();

            if (Directory.Exists(cookiesDir))
            {
                string[] files = Directory.GetFiles(cookiesDir);
                foreach (var file in files.OrderBy(t => new FileInfo(t).Name))
                {
                    FileInfo f = new FileInfo(file);
                    CookiesDict.Add(f.Name, ReadCookiesFromDisk(file));
                    cboSelectUser.Items.Add(f.Name);
                }
                if (files.Length > 0)
                    cboSelectUser.SelectedIndex = 0;
            }
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPayPWD.Text))
            {
                txtLogs.AppendText("请输入支付密码！！！！\r\n");
                return;
            }

            try
            {


                string file = null;
                if (rdoNewUser.Checked)
                {
                    string userName = txt_UserName.Text.Trim();
                    string pwd = txt_Password.Text.Trim();

                    string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string encryptPWD = Encrypt(dateStr + pwd, "oZ3qvmx4BTATvbI7", "oZ3qvmx4BTATvbI7");

                    var param = string.Format(@"format=json&appKey=00001&v=1.0&timestamp={0}&method=sso.login&origin=1&source=1&vcode=''&cookieAge=7200&u={1}&p={2}&synUrl=https://jr.yatang.cn/index.php?s=/index/index/*type:1", dateStr, userName, encryptPWD);

                    string res = WebHttpHeper.PostFormRequest(param, PageURL_Login, PageURL_LoginPost, ref cookie);
                    dynamic data = null;
                    data = JSON.DeserializeDynamic(res);
                    if (data.code == "0")
                    {
                        lblUserName.Text = txt_UserName.Text;
                        PnOptor.Show();
                        PnLogin.Hide();

                        if (!Directory.Exists(cookiesDir))
                        {
                            Directory.CreateDirectory(cookiesDir);
                        }
                        file = cookiesDir + "/" + lblUserName.Text;
                        WriteCookiesToDisk(file, cookie);

                        if (data.isRedirect)
                        {
                            SetUserCookieAnUserLoginStatus(data.redirectUrl.ToString().Trim('"'));
                            return;
                        }

                        //其他处理
                        return;
                    }
                    txtLogs.AppendText(data.message.ToString() + data.data + "\r\n");
                }
                else
                {

                    lblUserName.Text = cboSelectUser.SelectedItem.ToString();
                    cookie = CookiesDict[lblUserName.Text];
                    SetUserCookieAnUserLoginStatus("https://jr.yatang.cn/index.php?s=/index/index/*type:1");
                    PnLogin.Hide();
                    PnOptor.Show();
                }

                CookieCollection collection = cookie.GetCookies(new Uri(PageURL_Base, UriKind.Absolute));
                Cookie ckItem = collection["PayPWD"];
                if (ckItem == null)
                {
                    cookie.SetCookies(new Uri(PageURL_Base, UriKind.Absolute), "PayPWD=" + txtPayPWD.Text);
                    file = cookiesDir + "/" + lblUserName.Text;
                    WriteCookiesToDisk(file, cookie);
                }

                lblPayPWD.Text = txtPayPWD.Text;
            }
            catch (Exception ex)
            {
                txtLogs.AppendText("系统异常：" + ex.Message + "\r\n");
            }
        }

        public void SetUserCookieAnUserLoginStatus(string url)
        {
            Uri uri = new Uri(url);

            var loginRedirectURL = uri.Scheme + "://" + uri.Host + "/ytRedirect.html#" + url;
            //var ret = WebHttpHeper.GetRequest(loginRedirectURL, ref cookie);
            //txtLogs.AppendText("------登录跳转------\r\n");

            var setCookieUrl = uri.Scheme + "://" + uri.Host + "/Ajax/setUserCookie";
            //var rets = WebHttpHeper.PostJSONRequest(JSON.Serialize(new { url = url }), loginRedirectURL, setCookieUrl, ref cookie);
            var rets = WebHttpHeper.PostFormRequest("url=" + url, loginRedirectURL, setCookieUrl, ref cookie);
            txtLogs.AppendText("------登录成功-------\r\n");
        }

        Random r = new Random();
        // 融资标列表
        private void StartAutoInvest()
        {
            tmrRandFloat.Stop();
            //用户是否有红包
            bool luckMoneyDataIsNotNull = true;
            bool isInvestIng = false;
            try
            {
                //var result = WebHttpHeper.PostFormRequest("aprrange=1&selectdate=2&repaystyle=1&page_href=", "https://jr.yatang.cn/Financial/asset", "https://jr.yatang.cn/Financial/getAssetList", ref cookie);
                var result = WebHttpHeper.PostFormRequest("aprrange=0&selectdate=0&repaystyle=0&page_href=", "https://jr.yatang.cn/Financial/asset", "https://jr.yatang.cn/Financial/getAssetList", ref cookie);

                var financeData = JSON.DeserializeDynamic(result);
                txtLogs.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + financeData.ToString() + "\r\n");

                if (financeData.status == 1 && financeData.list.Count > 0)
                {
                    dynamic maxRemain = null;
                    List<dynamic> financeList = new List<dynamic>();
                    //抓取剩余金额最大的一个
                    foreach (var item in financeData.list)
                    {
                        if (maxRemain == null)
                        {
                            maxRemain = item;
                        }
                        else if (item.remain > maxRemain.remain)
                        {
                            maxRemain = item;
                        }
                        financeList.Add(item);
                    }

                    //投标 URL
                    string investDetailURL = PageURL_InvestDetail + maxRemain.id.ToString().Trim('"');

                    //请求页面，解析 html {hash,uniqKey}
                    var retHTML = WebHttpHeper.GetRequest(investDetailURL, ref cookie);

                    //获取用户红包
                    var data = GetUserLuckMoney(investDetailURL, maxRemain, out luckMoneyDataIsNotNull);

                    //没有可用红包继续轮询
                    if (data != null)
                    {
                        //自动投资
                        string name = maxRemain.name.ToString().Trim('"');
                        string ibnum = "121" + name.Substring(name.IndexOf('_') + 1);
                        InvestToRemote(investDetailURL, retHTML, ibnum, Convert.ToInt32(data.id.ToString().Trim('"')), Convert.ToInt32(data.user_constraint.ToString()));
                    }

                    //不使用红包
                    if (!chkUseLuckMoney.Checked)
                    {
                        //自动投资
                        string name = maxRemain.name.ToString().Trim('"');
                        string ibnum = "121" + name.Substring(name.IndexOf('_') + 1);
                        InvestToRemote(investDetailURL, retHTML, ibnum, 0, Convert.ToInt32(txtInvestValue.Text));
                        isInvestIng = true;
                    }
                }
            }
            catch (Exception ex)
            {
                txtLogs.AppendText("系统异常：" + ex.Message + "\r\n");
            }
            finally
            {
                //使用红包，不适用红包投资中
                if ((chkUseLuckMoney.Checked && luckMoneyDataIsNotNull) ||
                    (!chkUseLuckMoney.Checked && !isInvestIng))
                    tmrAutoInvest.Start();
            }
        }

        int luckMoneyRate = 50;

        // 用户数据，红包列表
        private dynamic GetUserLuckMoney(string headerURL, dynamic invest, out bool luckMoneyDataIsNotNull)
        {
            luckMoneyRate = Convert.ToInt32(txtLuckMoneyRate.Text);
            string name = invest.name.ToString().Trim('"');
            var retLuckMoney = WebHttpHeper.PostFormRequest("investMoney=&borrowNum=121" + name.Substring(name.IndexOf('_') + 1) + "&pageNum=1", headerURL, PageURL_GetUserCoupon, ref cookie);
            var data = JSON.DeserializeDynamic(retLuckMoney);

            luckMoneyDataIsNotNull = true;
            if (data.status == 0)
            {
                txtLogs.AppendText("---拉取红包" + Convert.ToString(data.info) + "\r\n");
                txtLogs.AppendText("---" + data.ToString() + "\r\n");

                string info = data.info.ToString().Trim('"');
                //判断是否登录过期
                if (info == "请先登陆")
                {
                    luckMoneyDataIsNotNull = false;
                    PnLogin.Visible = true;
                    PnOptor.Visible = false;
                }
                else luckMoneyDataIsNotNull = info != "暂无适合使用的红包";
            }

            if (data.data != null)
            {
                double withDrawal_Cash = Convert.ToDouble(data.withdrawal_cash.ToString().Trim('"'));

                dynamic luckMoney = null;
                foreach (var item in data.data)
                {
                    if (invest.remain < item.user_constraint) continue;//剩余金额小于红包金额

                    //红包比例
                    int currRate = (int)(item.user_constraint / item.value);
                    if (currRate <= luckMoneyRate)
                    {
                        double userConstraint = Convert.ToDouble(item.user_constraint.ToString());

                        //剩余可用红包金额大于，红包金额时
                        if (withDrawal_Cash > userConstraint)
                        {
                            //如果使用
                            if (luckMoney == null)
                            {
                                luckMoney = item;
                            }
                            else
                            {
                                int luckRate = (int)(luckMoney.user_constraint / luckMoney.value);
                                //红包比例更小
                                if (currRate < luckRate)
                                {
                                    //红包比例更小优先
                                    luckMoney = item;
                                    continue;
                                }
                                else if (currRate == luckRate)//红包比例相等，优先大额
                                {
                                    if (item.user_constraint > luckMoney.user_constraint)
                                    {
                                        luckMoney = item;
                                    }
                                    //红包比例相等，额度相等，优先快过期
                                    else if (item.user_constraint == luckMoney.user_constraint)
                                    {
                                        if (item.nearExpire == 1)
                                        {
                                            luckMoney = item;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return luckMoney;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">标链接</param>
        /// <param name="ibnum">标号</param>
        /// <param name="lunchId">红包ID</param>
        /// <param name="amount">投资金额</param>
        private void InvestToRemote(string url, string retHTML, string ibnum, int lunchId, int amount)//红包ID，投资金额(根据红包来)
        {
            //html 解析值
            var userID = ""; //用户ID; CUID
            var uniqKey = ""; //唯一key
            var _hash = ""; //hash 值

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(retHTML);

            HtmlNode cuidNode = document.GetElementbyId("cuid");
            userID = cuidNode.Attributes["value"].Value;

            HtmlNode uniqKeyNode = document.GetElementbyId("uniqKey");
            uniqKey = uniqKeyNode.Attributes["value"].Value;

            HtmlNode _hashNode = document.GetElementbyId("J_form_msg2").SelectSingleNode("//input[@name='__hash__']");
            _hash = _hashNode.Attributes["value"].Value;

            //密码加密
            //var ppay = Encrypt(txtPayPWD.Text, uniqKey); //支付密码

            Thread.Sleep(3 * 1000);

            string utf8Str = (string)webBrowser1.Document.InvokeScript("utf16to8", new string[] { txtPayPWD.Text });
            string xxteaEncryptStr = (string)webBrowser1.Document.InvokeScript("xxtea_encrypt", new string[] { utf8Str, uniqKey });
            string ppay = (string)webBrowser1.Document.InvokeScript("encode64", new string[] { xxteaEncryptStr });
            //encode64(xxtea_encrypt(utf16to8(ppay), $("#uniqKey").val()))
            //txtLogs.AppendText("utf8Str:" + utf8Str);
            //txtLogs.AppendText("xxteaEncryptStr:" + xxteaEncryptStr);
            txtLogs.AppendText("ppay:" + ppay);

            string paramStr = string.Format("p_pay={0}&user_id={1}&ibnum={2}&lunchId={3}&amount={4}&__hash__={5}&vcode=", ppay, userID, ibnum, lunchId, amount, _hash);

            txtLogs.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + paramStr + "\r\n");

            var result = WebHttpHeper.PostFormRequest(paramStr, url, PageURL_InvestCheckPPay, ref cookie);
            var resultText = JSON.DeserializeDynamic(result).ToString();
            txtLogs.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + resultText + "\r\n");
        }

        private static string Encrypt(string toEncrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        private static void WriteCookiesToDisk(string file, CookieContainer cookieJar)
        {
            using (Stream stream = File.Create(file))
            {
                try
                {
                    Console.Out.Write("Writing cookies to disk... ");
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, cookieJar);
                    Console.Out.WriteLine("Done.");
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine("Problem writing cookies to disk: " + e.GetType());
                }
            }
        }

        private static CookieContainer ReadCookiesFromDisk(string file)
        {
            try
            {
                using (Stream stream = File.Open(file, FileMode.Open))
                {
                    Console.Out.Write("Reading cookies from disk... ");
                    BinaryFormatter formatter = new BinaryFormatter();
                    Console.Out.WriteLine("Done.");
                    return (CookieContainer)formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Problem reading cookies from disk: " + e.GetType());
                return new CookieContainer();
            }
        }

        private void btnLoginOut_Click(object sender, EventArgs e)
        {
            tmrRandFloat.Stop();
            tmrAutoInvest.Stop();
            cookie = null;
            //File.Delete(cookiesDir + "/" + lblUserName.Text);

            PnOptor.Hide();
            PnLogin.Show();
        }

        /// <summary>
        /// 浮动timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrRandFloat_Tick(object sender, EventArgs e)
        {
            StartAutoInvest();
        }

        private void tmrAutoInvest_Tick(object sender, EventArgs e)
        {
            //停止固定计时器
            tmrAutoInvest.Stop();
            //开启随机计时器
            tmrRandFloat.Interval = r.Next(1, 3) * 1000;
            tmrRandFloat.Start();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tmrRandFloat.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tmrRandFloat.Stop();
            tmrAutoInvest.Stop();
        }

        private void chkUseLuckMoney_CheckedChanged(object sender, EventArgs e)
        {
            txtInvestValue.Enabled = !chkUseLuckMoney.Checked;
        }

        private void rdoUser_CheckedChanged(object sender, EventArgs e)
        {
            var isNewUser = ((RadioButton)sender) == rdoNewUser;
            txt_UserName.Enabled = isNewUser;
            txt_Password.Enabled = isNewUser;

            cboSelectUser.Enabled = !isNewUser;
            if (!isNewUser) ReLoadCookieUsers();
        }

        private void cboSelectUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPayPWD.Text = string.Empty;
            string userName = ((ComboBox)sender).SelectedItem.ToString();
            CookieCollection collection = CookiesDict[userName].GetCookies(new Uri(PageURL_Base, UriKind.Absolute));
            Cookie ckItem = collection["PayPWD"];
            if (ckItem != null)
            {
                txtPayPWD.Text = ckItem.Value;
            }
        }
    }


    public enum XXTEAMode
    {
        Encrypt,
        Decrypt
    }

    static public class XXTEA
    {
        static public byte[] Code(byte[] data, uint[] k, XXTEAMode mode)
        {
            uint[] v = new uint[(int)Math.Ceiling((float)data.Length / 4)];
            Buffer.BlockCopy(data, 0, v, 0, data.Length);

            unchecked
            {
                const uint DELTA = 0x9e3779b9;
                uint y = 0, z = 0, sum = 0, p = 0, rounds = 0, e = 0;
                int n = v.Length;
                Func<uint> MX = () => (((z >> 5 ^ y << 2) + (y >> 3 ^ z << 4)) ^ ((sum ^ y) + (k[(p & 3) ^ e] ^ z)));

                if (mode == XXTEAMode.Encrypt)
                {
                    rounds = (uint)(6 + 52 / n);
                    z = v[n - 1];
                    do
                    {
                        sum += DELTA;
                        e = (sum >> 2) & 3;
                        for (p = 0; p < n - 1; p++)
                        {
                            y = v[p + 1];
                            z = v[p] += MX();
                        }
                        y = v[0];
                        z = v[n - 1] += MX();
                    } while (--rounds > 0);
                }
                else
                {
                    rounds = (uint)(6 + 52 / n);
                    sum = rounds * DELTA;
                    y = v[0];
                    do
                    {
                        e = (sum >> 2) & 3;
                        for (p = (uint)(n - 1); p > 0; p--)
                        {
                            z = v[p - 1];
                            y = v[p] -= MX();
                        }
                        z = v[n - 1];
                        y = v[0] -= MX();
                    } while ((sum -= DELTA) != 0);
                }
            }

            byte[] rvl = new byte[v.Length * 4];
            Buffer.BlockCopy(v, 0, rvl, 0, rvl.Length);
            return rvl;
        }
    }
}
