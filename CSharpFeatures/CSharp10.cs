using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CSharpFeatures.CSharp8;

namespace CSharpFeatures
{
    internal class CSharp10
    {
        public void NewFeatures()
        {
            // Record structs

            // Improvements of structure types

            // Interpolated string handlers

            // global using directives

            // File - scoped namespace declaration
            // namespace MyNamespace;

            // Extended property patterns in C# 10 not 8
            // { Prop1: { Prop2: pattern } }

            // Improvements on lambda expressions in C# 10 const interpolated string
            // Allow const interpolated strings
            // Record types can seal ToString()
            // Improved definite assignment
            AssignmentBefore();
            AssignmentInCSharp10();
            // Allow both assignment and declaration in the same deconstruction
            // Allow AsyncMethodBuilder attribute on methods
            // CallerArgumentExpression attribute
            // Enhanced
            // #line pragma
            // Warning wave 6
        }

        private void AssignmentInCSharp10()
        {
            var point = new Point(1, 2);

            int x = 0;
            (x, int y) = point;
        }

        private void AssignmentBefore()
        {
            // Initialization:
            var point = new Point(1, 2);
            (int x, int y) = point;

            // assignment:
            int x1 = 0;
            int y1 = 0;
            (x1, y1) = point;
        }
    }
}
