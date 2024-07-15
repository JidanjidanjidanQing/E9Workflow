using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EcologyWorkflowService
{
    public class SSOWeaver
    {
        // POST请求URL
        const string url = "http://192.168.39.71:8008";

        //getToken路由
        const string url_getToken = "/ssologin/getToken";

        //url前部
        const string url_head = "/spa/workflow/index_form.jsp?ssoToken=";

        //url后部
        const string url_tail = "#/main/workflow/req?requestid=";

        //泛微后台配置的appid
        const string appid = "f05e41af-959e-4c76-8291-ad0ca8fad8fe";

        public SSOWeaver() { }

        public async Task<String> SSO(string loginid, string requestid)
        {
            Console.WriteLine("requestid="+requestid);

            // 创建HttpClient实例
            using (HttpClient client = new HttpClient())
            {
                // 创建一个字典来存储要发送的表单数据
                var formData = new Dictionary<string, string>
                {
                    { "appid", appid },
                    { "loginid", loginid }
                };

                // 将字典转换为表单URL编码的字符串
                var content = new FormUrlEncodedContent(formData);

                // 设置Content-Type为application/x-www-form-urlencoded
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");


               try
               {
                    // 发送POST请求
                    HttpResponseMessage response = await client.PostAsync(url+url_getToken, content);

                    // 确保HTTP成功状态值
                    response.EnsureSuccessStatusCode();

                    // 读取并输出响应内容
                    string responseBody = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("token="+responseBody);

                    //输出地址，拼接url、token、requestid
                    Console.WriteLine(url + url_head + responseBody + url_tail + requestid);

                    //调用系统默认浏览器
                    Process.Start(new ProcessStartInfo(url + url_head + responseBody + url_tail + requestid) { UseShellExecute = true });


                    return responseBody;

               }
               catch (HttpRequestException e)
               {
                    // 处理异常
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);

                    return "Get Token Failure";
               }
            }
        }
    }
}
