using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinCan;

namespace CerebrumLogger
{
    public class xCall
    {
        public Agent agent { get; }
        public Activity activity { get; private set; }
        public Verb verb { get; }
        public Context context { get; private set; }
        public Result result { get; private set; }
        public Statement statement { get; private set; }
        public string url { get; set; } = "http://gamestrax.com/";

        /// <summary>
        /// Simplest xCall instantiation that can exist includes only a learner name, id, and verb.
        /// We can add other elements to this xCall object using the methods of this class.
        /// </summary>
        /// <param name="learner_name"></param>
        /// <param name="learner_id"></param>
        /// <param name="verb_string"></param>
        public xCall(String learner_name, String learner_id, String verb_string)
        {
            this.agent = SetAgent(learner_name, learner_id);
            this.verb = SetVerb(verb_string);
        }

        /// <summary>
        /// Instantiation of xCall object where we want to specify a different URL
        /// </summary>
        /// <param name="learner_name"></param>
        /// <param name="learner_id"></param>
        /// <param name="verb_string"></param>
        /// <param name="url"></param>
        public xCall(String learner_name, String learner_id, String verb_string, String url)
        {
            this.agent = SetAgent(learner_name, learner_id);
            this.verb = SetVerb(verb_string);
            this.url = url;
        }

        /// <summary>
        /// Sets the xCall's agent object using learner name and id
        /// </summary>
        /// <param name="learner_name"></param>
        /// <param name="learner_id"></param>
        /// <returns>TinCan.Agent object</returns>
        private Agent SetAgent(String learner_name, String learner_id)
        {
            Agent agent = new Agent();
            agent.mbox = "mailto:" + learner_id + "@sg-tap.com";
            agent.name = learner_name;
            return agent;
        }

        /// <summary>
        /// Sets the xCall's verb to the specified string
        /// </summary>
        /// <param name="verb_string"></param>
        /// <returns>TinCan.Verb object</returns>
        private Verb SetVerb(String verb_string)
        {
            Verb verb = new Verb();
            verb.id = new Uri(url + "verbs/" + verb_string);;
            return verb;
        }

        /// <summary>
        /// Sets the TinCan activity to the specified game.  A game name is the minimum information that must be passed to SetActivity.
        /// </summary>
        /// <param name="gameName"></param>
        public void SetActivity(String gameName)
        {
            Activity activity = new Activity();
            activity.id = url + "games/" + gameName;
            activity.definition = new ActivityDefinition();
            activity.definition.type = new Uri(url + "define/type/game");
            this.activity = activity;
        }

        /// <summary>
        /// Sets the TinCan activity to the specified game at the specified stage.
        /// </summary>
        /// <param name="gameName"></param>
        /// <param name="stageName"></param>
        public void SetActivity(String gameName, String stageName)
        {
            Activity activity = new Activity();
            activity.id = url + "games/" + gameName + "/stages/" + stageName;
            activity.definition = new ActivityDefinition();
            activity.definition.type = new Uri(url + "define/type/stage");
            this.activity = activity;
        }

        /// <summary>
        /// Sets the TinCan activity to the specified game, stage, and activity name
        /// </summary>
        /// <param name="gameName"></param>
        /// <param name="stageName"></param>
        /// <param name="activityName"></param>
        public void SetActivity(String gameName, String stageName, String activityName)
        {
            Activity activity = new Activity();
            activity.id = url + "games/" + gameName + "/stages/" + stageName + "/activities/" + activityName;
            activity.definition = new ActivityDefinition();
            activity.definition.type = new Uri(url + "define/type/activity");
            this.activity = activity;
        }

        /// <summary>
        /// Sets the TinCan activity to the specified game, stage, activity name, with specified option
        /// </summary>
        /// <param name="gameName"></param>
        /// <param name="stageName"></param>
        /// <param name="activityName"></param>
        /// <param name="option"></param>
        public void SetActivity(String gameName, String stageName, String activityName, String option)
        {
            Activity activity = new Activity();
            activity.id = url + "games/" + gameName + "/stages/" + stageName + "/activities/" + activityName + "/options/" + option;
            activity.definition = new ActivityDefinition();
            activity.definition.type = new Uri(url + "define/type/option");
            this.activity = activity;
        }

        /// <summary>
        /// Constructs a TinCan.Extensions object from a JObject
        /// </summary>
        /// <param name="jobj"></param>
        /// <returns>TinCan.Extensions object</returns>
        public TinCan.Extensions BuildExtensions(JObject jobj)
        {
            TinCan.Extensions ext = new TinCan.Extensions(jobj);
            return ext;
        }

        /// <summary>
        /// Sets an xCall's context to a new context with the specified TinCan.Extensions object
        /// </summary>
        /// <param name="extensions"></param>
        public void SetContext(TinCan.Extensions extensions)
        {
            Context context = new Context();
            context.extensions = extensions;
            this.context = context;
        }

        /// <summary>
        /// Adds the specified TinCan.Extensions object to the xCall's context
        /// </summary>
        /// <param name="extensions"></param>
        public void AddExtensionToContext(TinCan.Extensions extensions)
        {
            this.context.extensions = extensions;
        }

        /// <summary>
        /// Adds the specified TinCan.Extensions object to the xCall's result
        /// </summary>
        /// <param name="extensions"></param>
        public void AddExtensionToResult(TinCan.Extensions extensions) 
        {
            this.result.extensions = extensions;
        }

        /// <summary>
        /// Sets the xCall result's success attribute
        /// </summary>
        /// <param name="success"></param>
        public void SetResult(bool success)
        {
            Result result = new Result();
            result.success = success;
            this.result = result;
        }

        /// <summary>
        /// Sets the xCall result's success. Also includes and sets max, min, and raw scores
        /// </summary>
        /// <param name="success"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <param name="raw"></param>
        public void SetResult(bool success, int max, int min, int raw)
        {
            Result result = new Result();
            result.success = success;
            Score score = new Score();
            score.max = max;
            score.min = min;
            score.raw = raw;
            result.score = score;
            this.result = result;
        }

        /// <summary>
        /// Turns an xCall object into a TinCan.Statement object.  APIHandler.cs uses BuildStatement() with the specified xCall input to send an API call.
        /// </summary>
        /// <returns>TinCan.Statement object</returns>
        public Statement BuildStatement()
        {
            Statement statement = new Statement
            {
                verb = this.verb,
                actor = this.agent,
                target = this.activity
            };
            if (this.context != null)
            {
                statement.context = this.context;
            }
            if(this.result != null)
            {
                statement.result = this.result;
            }

            statement.Stamp();

            this.statement = statement;
            return statement;
        }


    }
}
