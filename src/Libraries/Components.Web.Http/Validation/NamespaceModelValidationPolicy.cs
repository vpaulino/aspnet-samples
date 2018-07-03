using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Web.Http.Validation
{
    public class NamespaceModelValidationPolicy : ModelValidationPolicy
    {
        private Dictionary<ComparisonRule, Func<Type, string, bool>> comparisonHandlers = new Dictionary<ComparisonRule, Func<Type, string, bool>>();
        private string namespaceToCompare;
        private ComparisonRule Rule;


        /// <summary>
        /// Creates a instance of NamespaceModelValidationPolicy Policy 
        /// </summary>
        /// <param name="predicate">comparison predicate</param>
        /// <param name="shouldValidate">decides the result of evaluation. if true and if rule mathces then validation will occour, if false it will not do model validation</param>
        public NamespaceModelValidationPolicy(string namespaceToCompare, bool shouldValidate) : base(shouldValidate)
        {
            this.Rule = ComparisonRule.Equals;
            this.namespaceToCompare = namespaceToCompare;
            comparisonHandlers.Add(ComparisonRule.Contains, (type, toCompare) => !type.Namespace.Contains(toCompare.ToString()));
            comparisonHandlers.Add(ComparisonRule.EndWith, (type, toCompare) => !type.Namespace.EndsWith(toCompare.ToString()));
            comparisonHandlers.Add(ComparisonRule.StartWith, (type, toCompare) => !type.Namespace.StartsWith(toCompare.ToString()));
            comparisonHandlers.Add(ComparisonRule.Equals, (type, toCompare) => !type.Namespace.Equals(toCompare.ToString()));
        }


        public NamespaceModelValidationPolicy(string namespaceToCompare, ComparisonRule rule, bool shouldValidate = true) : this(namespaceToCompare, shouldValidate)
        {
            this.Rule = rule;
            this.namespaceToCompare = namespaceToCompare;
        }

        public override bool Evaluate(Type type)
        {
            Func<Type, string, bool> comparisonHandler;

            if (comparisonHandlers.TryGetValue(this.Rule, out comparisonHandler))
            {
                return this.shouldValidate && comparisonHandler(type, this.namespaceToCompare);
            }

            return true;
        }
    }
}
