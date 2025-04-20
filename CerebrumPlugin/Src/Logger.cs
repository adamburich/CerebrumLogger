namespace CerebrumLogger
{
	using System;
	using TinCan;
	using TinCan.LRSResponses;

	public class Logger
	{
		private RemoteLRS lrs;
		private Queue<Statement> logQueue;
		private RemoteLRS moodleLRS;

        /// <summary>
        /// Constructor for a Logger object.  Must be passed username and password strings to access and send requests via xAPI/TinCan
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public Logger(string username, string password, string moodleEndpoint, string moodleUsername, string moodlePassword)
        {
            this.logQueue = new Queue<Statement>();
            this.lrs = new RemoteLRS(
                "https://ll.sg-tap.com/data/xAPI/statements", // LearningLocker endpoint
                username,
                password
            );

            // Initialize Moodle LRS
            this.moodleLRS = new RemoteLRS(
                moodleEndpoint, // Moodle xAPI endpoint
                moodleUsername,
                moodlePassword
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
				DoSendAPI_LL(this.logQueue.Dequeue());
            }
        }

		/// <summary>
		/// Constructs a TinCan.Statement object from a given string of json, then sends the statement to the API.
		/// </summary>
		/// <param name="jsonData"></param>
		public void DoSendAPI_LL(string jsonData)
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
		public void DoSendAPI_LL(Statement statement)
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

        /// <summary>
        /// Sends a statement to Moodle SCORM module
        /// </summary>
        /// <param name="statement"></param>
        public void DoSendAPI_SCORM(Statement statement)
        {
            StatementLRSResponse response = moodleLRS.SaveStatement(statement);
            if (response.success)
            {
                Console.WriteLine("SCORM statement sent to Moodle: " + response.content.id);
            }
            else
            {
                Console.WriteLine("Failed to send SCORM statement to Moodle: " + response.errMsg);
            }
        }

        /// <summary>
        /// Constructs a TinCan.Statement object from a given string of json, then sends the statement to SCORM.
        /// </summary>
        /// <param name="statement"></param>
        public void DoSendAPI_SCORM(string jsonData)
        {
            Statement statement = JsonToTinCan.toStatement(jsonData);
            StatementLRSResponse response = moodleLRS.SaveStatement(statement);

            if (response.success)
            {
                Console.WriteLine("SCORM statement sent to Moodle: " + response.content.id);
            }
            else
            {
                Console.WriteLine("Failed to send SCORM statement to Moodle: " + response.errMsg);
            }
        }

        /// <summary>
        /// Sends to LL and SCORM together given a statement object.
        /// </summary>
        /// <param name="statement"></param>
        public void DoSendAPI(Statement statement)
        {
            DoSendAPI_LL(statement);
            DoSendAPI_SCORM(statement);
        }

        /// <summary>
        /// Sends to LL and SCORM together given a json string.
        /// </summary>
        /// <param name="statement"></param>
        public void DoSendAPI(string jsonData)
        {
            DoSendAPI_LL(jsonData);
            DoSendAPI_SCORM(jsonData);
        }

    }
}

