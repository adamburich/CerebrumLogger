namespace CerebrumLogger
{
    using System;
    using TinCan;
    using TinCan.LRSResponses;

    public class APIHandler
    {
        private string url = "https://ll.sg-tap.com/data/xAPI/";
        private RemoteLRS lrs;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public APIHandler(string username, string password)
        {
            this.lrs = new RemoteLRS(url, username, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="call"></param>
        /// <returns></returns>
        public string SendCall(xCall call)
        {
            Statement statement = call.BuildStatement();
            StatementLRSResponse response = lrs.SaveStatement(statement);
            if (response.success)
            {
                // Updated 'statement' here, now with id
                //return 0;
                return response.content.ToString();
            }
            else
            {
                // Do something with failure
                //return -1;
                return response.errMsg;
            }
        }

    }
}