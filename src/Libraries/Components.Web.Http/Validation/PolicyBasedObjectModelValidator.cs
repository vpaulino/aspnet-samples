using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if NET46
using System.Web.Http.Metadata;
using System.Web.Http.Validation;
#endif

namespace Components.Web.Http.Validation
{

#if NET46
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

        protected override bool ValidateProperties(ModelMetadata metadata, BodyModelValidatorContext validationContext)
        {
            var result = base.ValidateProperties(metadata, validationContext);

            return result;
        }
    }
#endif
}
