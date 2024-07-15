using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading.Tasks;
using weaver;
using System.IO;

/*
    版本：1.00
    时间：2024.05.18
    目的：通过webservice创建泛微流程，并单点登录打开泛微流程页面
    开发人员：张钰青
 */

namespace EcologyWorkflowService
{
    class Program
    {
        //营销公司id
        static string[] fb = { "381" };

        //流程id
        private static string workflowId = "122";

        //流程名称
        private static string workflowName = "销售计划填报流程-自动提交-webservice测试";

        //发起流程的用户id
        private static int userId = 321;

        static async Task Main(string[] args)
        {
            //workflowId=Console.ReadLine();
            //workflowName = Console.ReadLine();
            //userId = int.Parse(Console.ReadLine());

            //workflowId = args[0];
            //workflowName = args[1];
            //userId = int.Parse(args[2]);

            WorkflowBaseInfo workflowBaseInfo = new WorkflowBaseInfo();//工作流信息
            workflowBaseInfo.workflowId = workflowId;//流程ID
            workflowBaseInfo.workflowName = workflowName;//流程名称


            WorkflowRequestInfo workflowRequestInfo = new WorkflowRequestInfo();//工作流程请求信息

            workflowRequestInfo.canEdit = true;
            workflowRequestInfo.canView = true;
            workflowRequestInfo.requestLevel = "0";
            workflowRequestInfo.creatorId = userId.ToString();
            workflowRequestInfo.requestName = $"{workflowName}-webservice接口-{ DateTime.Now:yyyy-MM-dd}";
            workflowRequestInfo.workflowBaseInfo = workflowBaseInfo;

            //控制流程流转：为 0 时，保存但不提交；为 1 时，提交流程
            workflowRequestInfo.isnextflow = "1";

            #region 创建流程信息

            //创建主表字段信息
            List<WorkflowRequestTableField> mainTableField01 = new List<WorkflowRequestTableField>
            {
                //new WorkflowRequestTableField
                //{
                //    fieldName = "sqr",
                //    fieldValue = userId.ToString(),
                //    view = true,
                //    edit = true
                //} ,
                //new WorkflowRequestTableField
                //{
                //    fieldName = "sqrq",
                //    fieldValue = DateTime.Now.ToString("yyyy-MM-dd"),
                //    view = true,
                //    edit = true
                //} ,
                //new WorkflowRequestTableField
                //{
                //    fieldName = "szgs",
                //    fieldValue = "243",
                //    view = true,
                //    edit = true
                //} ,
                //new WorkflowRequestTableField
                //{
                //    fieldName = "szbm",
                //    fieldValue = "1448",
                //    view = true,
                //    edit = true
                //} ,
                //new WorkflowRequestTableField
                //{
                //    fieldName = "gc1",
                //    fieldValue = "5",
                //    view = true,
                //    edit = true
                //} ,
                //new WorkflowRequestTableField
                //{
                //    fieldName = "kcdd1",
                //    fieldValue = "600",
                //    view = true,
                //    edit = true
                //} ,
                //new WorkflowRequestTableField
                //{
                //    fieldName = "bm",
                //    fieldValue = "10",
                //    view = true,
                //    edit = true
                //} ,
                new WorkflowRequestTableField
                {
                    fieldName = "fb",
                    fieldValue = "381",
                    view = true,
                    edit = true
                } 
                //new WorkflowRequestTableField
                //{
                //    fieldName = "basj",
                //    fieldValue = "2023-12-20",
                //    view = true,
                //    edit = true
                //} ,
                //new WorkflowRequestTableField
                //{
                //    fieldName = "baxm",
                //    fieldValue = "webservice测试",
                //    view = true,
                //    edit = true
                //} ,
                //new WorkflowRequestTableField
                //{
                //    fieldName = "sjms",
                //    fieldValue = "webservice测试",
                //    view = true,
                //    edit = true
                //} ,
            };

            //创建明细表字段信息
            List<WorkflowRequestTableField> detailTableField01 = new List<WorkflowRequestTableField>
            {
                //new WorkflowRequestTableField
                //{
                //    fieldName = "wl",
                //    fieldValue = "3011100017",
                //    view = true,
                //    edit = true
                //} ,
                //new WorkflowRequestTableField
                //{
                //    fieldName = "wlms",
                //    fieldValue = "二级板_6-12_A_负",
                //    view = true,
                //    edit = true
                //} ,
                //new WorkflowRequestTableField
                //{
                //    fieldName = "dw",
                //    fieldValue = "PC",
                //    view = true,
                //    edit = true
                //} ,
                //new WorkflowRequestTableField
                //{
                //    fieldName = "wlzygz1",
                //    fieldValue = "3011100014",
                //    view = true,
                //    edit = true
                //} ,
                //new WorkflowRequestTableField
                //{
                //    fieldName = "wlmszygz",
                //    fieldValue = "二级板_8-10_A_正",
                //    view = true,
                //    edit = true
                //} ,
                //new WorkflowRequestTableField
                //{
                //    fieldName = "dw1",
                //    fieldValue = "PC",
                //    view = true,
                //    edit = true
                //} ,
                //new WorkflowRequestTableField
                //{
                //    fieldName = "sl",
                //    fieldValue = "10",
                //    view = true,
                //    edit = true
                //}
            };

            //添加表单主表记录
            List<WorkflowRequestTableRecord> mainTableRecord = new List<WorkflowRequestTableRecord>
            {
                new WorkflowRequestTableRecord
                {
                    workflowRequestTableFields = mainTableField01.ToArray()
                }
            };

            //添加表单明细记录
            //List<WorkflowRequestTableRecord> detailTableRecord = new List<WorkflowRequestTableRecord>
            //{
            //    new WorkflowRequestTableRecord
            //    {
            //        workflowRequestTableFields = detailTableField01.ToArray()
            //    }
            //};

            //主表构造赋值
            WorkflowMainTableInfo mainTableInfo = new WorkflowMainTableInfo
            {
                requestRecords = mainTableRecord.ToArray(),
            };

            //明细表构造赋值
            //List<WorkflowDetailTableInfo> detailTableInfo = new List<WorkflowDetailTableInfo>
            //{
            //    new WorkflowDetailTableInfo
            //    {
            //            workflowRequestTableRecords = detailTableRecord.ToArray()
            //    }
            //};

            workflowRequestInfo.workflowMainTableInfo = mainTableInfo;
            //workflowRequestInfo.workflowDetailTableInfos = detailTableInfo.ToArray();

            #endregion

            //创建明细表信息

            #region 开始调用接口
            // 创建 HTTP 绑定对象
            var binding = new BasicHttpBinding
            {
                //设置最大传输接受数量
                MaxReceivedMessageSize = 2147483647
            };

            // 根据 WebService 的 URL 构建终端点对象
            var endpoint = new EndpointAddress("http://192.168.8.218:5221/services/WorkflowService");

            // 创建调用接口的工厂，注意这里泛型只能传入接口 添加服务引用时生成的 webservice的接口 一般是 (XXXSoap)
            var factory = new ChannelFactory<WorkflowServicePortType>(binding, endpoint);

            var client = factory.CreateChannel();


            //SSOWeaver ssoweaver = new SSOWeaver();

            //调用接口，创建流程并提交
            string response = await client.doCreateWorkflowRequestAsync(workflowRequestInfo, userId);

            Console.WriteLine(response);
            //using (StreamWriter writer = new StreamWriter("./销售计划填报流程自动发起日志.txt", true)
            //{
            //    writer.WriteLine("Another line");
            //    //$"{workflowName}-webservice接口-{DateTime.Now:yyyy-MM-dd}"
            //}

            string filePath = "./销售计划填报流程自动发起日志.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd--HH:mm:ss}--{fb[0]}--{response}");
            }



            //Console.WriteLine(await client.doCreateWorkflowRequestAsync(workflowRequestInfo, userId));


            #endregion
            Console.ReadKey() ;
        }
    }
}
