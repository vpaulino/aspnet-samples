using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Validation;

namespace Components.Web.Http.Validation
{


    public class PolicyBasedObjectModelValidator : DefaultBodyModelValidator
    {

        protected ModelValidationPolicy policy;


        public PolicyBasedObjectModelValidator(ModelValidationPolicy policy)
        {
            this.policy = policy;
        }


        public override bool ShouldValidateType(Type type)
        {
            return this.policy.Evaluate(type);
        }
    }
}
