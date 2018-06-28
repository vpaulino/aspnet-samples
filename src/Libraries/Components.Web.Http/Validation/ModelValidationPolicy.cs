using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Components.Web.Http.Validation
{
    /// <summary>
    /// Abstract class that describes what a model validation policy must be
    /// </summary>
    public abstract class ModelValidationPolicy
    {
        protected bool shouldValidate;

        /// <summary>
        /// Creates an instance of ModelValidationPolicy
        /// </summary>
        /// <param name="shouldValidate">indicates if result of the policy evaluation should result in aspNet validation model validation execution</param>
        public ModelValidationPolicy(bool shouldValidate = true)
        {
            this.shouldValidate = shouldValidate;
        }

        public abstract bool Evaluate(Type type);

    }
}
