using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using CerebrumLogger.Src.Scorm;

namespace CerebrumLogger
{
    public class DragDropQuestion : xCall
    {
        public DragDropQuestion(String learner_name, String learner_id, String verb_string)
        :
            base(learner_name, learner_id, verb_string)
        { }
        
    }
}
