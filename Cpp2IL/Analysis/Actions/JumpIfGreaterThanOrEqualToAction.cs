﻿using Cpp2IL.Analysis.ResultModels;
using Iced.Intel;

namespace Cpp2IL.Analysis.Actions
{
    public class JumpIfGreaterThanOrEqualToAction : ConditionalJumpAction
    {
        public JumpIfGreaterThanOrEqualToAction(MethodAnalysis context, Instruction instruction) : base(context, instruction)
        {
            //All handled by base class
        }

        public override Mono.Cecil.Cil.Instruction[] ToILInstructions()
        {
            throw new System.NotImplementedException();
        }

        protected override string GetPseudocodeCondition()
        {
            //Invert condition, so less than, not >=
            return $"({GetArgumentOnePseudocodeValue()} < {GetArgumentTwoPseudocodeValue()})";
        }

        protected override string GetTextSummaryCondition()
        {
            if (associatedCompare == null)
                return "the compare showed that it was greater than or equal";

            return $"{associatedCompare.ArgumentOne} is greater than or equal to {associatedCompare.ArgumentTwo}";
        }
    }
}