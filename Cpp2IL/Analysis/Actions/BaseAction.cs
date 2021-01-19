﻿using System.Text;
using Cpp2IL.Analysis.ResultModels;
using Iced.Intel;

namespace Cpp2IL.Analysis.Actions
{
    public abstract class BaseAction
    {
        private StringBuilder _lineComments = new StringBuilder();
        public Instruction AssociatedInstruction;
        
        public BaseAction(MethodAnalysis context, Instruction instruction)
        {
            AssociatedInstruction = instruction;
        }

        public abstract Mono.Cecil.Cil.Instruction[] ToILInstructions();

        public abstract string? ToPsuedoCode();

        public abstract string ToTextSummary();

        public string GetSynopsisEntry()
        {
            var comment = GetLineComment();

            if (string.IsNullOrWhiteSpace(comment))
                return ToTextSummary();
            
            return $"{ToTextSummary()} ; {GetLineComment()}";
        }

        protected void AddComment(string comment)
        {
            _lineComments.Append(" - ").Append(comment);
        }

        public string GetLineComment()
        {
            return _lineComments.ToString();
        }
    }
}