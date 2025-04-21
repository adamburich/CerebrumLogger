using System;
using System.Collections.Generic;
using UnityEngine;

namespace CerebrumLogger
{

    [Serializable]
    public class ObjectivesData {

        [SerializeField]
        private List<ObjectiveData> objectives = new List<ObjectiveData>();

        public List<ObjectiveData> Objectives {
            get { return objectives; }
        }

    }

}