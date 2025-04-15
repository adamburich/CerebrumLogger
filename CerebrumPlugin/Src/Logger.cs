namespace CerebrumLogger
{
	using System;
	using TinCan;
	using TinCan.LRSResponses;

	public class Logger
	{
		private RemoteLRS lrs;
		private Queue<Statement> logQueue;

		/// <summary>
		/// Constructor for a Logger object.  Must be passed username and password strings to access and send requests via xAPI/TinCan
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		public Logger(string username, string password)
		{
			this.logQueue = new Queue<Statement>();
			this.lrs = new RemoteLRS(
				"https://ll.sg-tap.com/data/xAPI/statements",
				username,
				password
			);
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

		//Can use in unity with the help of a CallBuilder object like: Logger.DoSendAPI(myCallBuilder.statement)

		/// <summary>
		/// Sends the specified statement call.
		/// </summary>
		/// <param name="statement"></param>
		public void DoSendAPI(Statement statement)
		{
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

	}
}

