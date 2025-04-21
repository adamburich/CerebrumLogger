namespace CerebrumLogger
{
	using System;
	using TinCan;
	using TinCan.LRSResponses;
	

	public class Logger
	{
		private RemoteLRS lrs;
		private Queue<Statement> logQueue;
		private ScormService scormService;

		/// <summary>
		/// Constructor for a Logger object.  Must be passed username and password strings to access and send requests via xAPI/TinCan
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		public Logger(string username, string password, ScormService scormService)
		{
			this.logQueue = new Queue<Statement>();
			this.lrs = new RemoteLRS(
				"https://ll.sg-tap.com/data/xAPI/statements",
				username,
				password
			);
            this.scormService = scormService;
        }

		/// <summary>
		/// Adds a log statement to the log queue.  Log queue will be dispatched when the queue reaches 5 items in length.
		/// </summary>
		/// <param name="statement"></param>
		public void AddLogToQueue(Statement statement)
        {
			logQueue.Enqueue(statement);
			if(logQueue.Count > 4)
            {
				SendLogBatch();
            }
        }

		/// <summary>
		/// Empties the log queue and sends the batch of log statements.
		/// </summary>
		private void SendLogBatch()
        {
			while(this.logQueue.Count > 0)
            {
				DoSendAPI(this.logQueue.Dequeue());
            }
        }

		/// <summary>
		/// Constructs a TinCan.Statement object from a given string of json, then sends the statement to the API.
		/// </summary>
		/// <param name="jsonData"></param>
		public void DoSendAPI(string jsonData)
		{
			Statement statement = JsonToTinCan.toStatement(jsonData);
			DoSendAPI(statement);
		}

		//Can use in unity with the help of a CallBuilder object like: Logger.DoSendAPI(myCallBuilder.statement)

		/// <summary>
		/// Sends the specified statement call.
		/// </summary>
		/// <param name="statement"></param>
		public void DoSendAPI(Statement statement)
		{
			SendMoodleSCORMLog(statement);
			StatementLRSResponse response = lrs.SaveStatement(statement);
			if (response.success)
			{
				// Updated 'statement' here, now with id
				Console.WriteLine("Save statement: " + response.content.id);
			}
			else
			{
				// Do something with failure
			}
		}

        /// <summary>
        /// Send log to Moodle/SCORM
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        public string SendMoodleSCORMLog(Statement statement)
        {
            //send actor
            //send timestamp
            //send verb
            //send context

            return "";
        }

    }
}

