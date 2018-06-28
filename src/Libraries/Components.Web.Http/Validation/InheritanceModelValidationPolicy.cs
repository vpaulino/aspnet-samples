using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Web.Http.Validation
{
    /// <summary>
    /// policy to be used in <see cref="VBObjectModelValidator"/> to evaluate if some type must go to AspNet Model Validation.
    /// This Policy evaluates if the argument to be evaluated inherits from predicate <see cref="Type"/>
    /// </summary>
    public class InheritanceModelValidationPolicy : ModelValidationPolicy
    {
        protected Type predicate;

        /// <summary>
        /// Creates a instance of InheritanceModelValidationPolicy Policy 
        /// </summary>
        /// <param name="predicate">comparison predicate</param>
        /// <param name="shouldValidate">decides the result of evaluation. if true and if inheritance exists the validation will happen, if false it will not do model validation</param>
        public InheritanceModelValidationPolicy(Type predicate, bool shouldValidate = true) : base(shouldValidate)
        {
            this.predicate = predicate;
        }

        public override bool Evaluate(Type type)
        {
            return shouldValidate && predicate.IsAssignableFrom(type);
        }
    }
}
