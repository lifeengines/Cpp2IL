﻿using Cpp2IL.Analysis.ResultModels;
using Iced.Intel;
using Mono.Cecil;

namespace Cpp2IL.Analysis.Actions
{
    public class StaticFieldOffsetToRegAction : BaseAction
    {
        private StaticFieldsPtr? _staticFieldPtrObject;
        private string? _destReg;
        private ConstantDefinition? _constantMade;

        public StaticFieldOffsetToRegAction(MethodAnalysis context, Instruction instruction) : base(context, instruction)
        {
            //Get the type we're moving from
            var theConstant = context.GetConstantInReg(Utils.GetRegisterNameNew(instruction.MemoryBase));
            _destReg = Utils.GetRegisterNameNew(instruction.Op0Register);

            if (theConstant == null || theConstant.Type != typeof(TypeDefinition)) return;

            var typeFieldsAreFor = (TypeDefinition) theConstant.Value;
            _staticFieldPtrObject = new StaticFieldsPtr(typeFieldsAreFor);

            _constantMade = context.MakeConstant(typeof(StaticFieldsPtr), _staticFieldPtrObject, reg: _destReg);
        }

        public override Mono.Cecil.Cil.Instruction[] ToILInstructions()
        {
            throw new System.NotImplementedException();
        }

        public override string? ToPsuedoCode()
        {
            throw new System.NotImplementedException();
        }

        public override string ToTextSummary()
        {
            return $"Loads the pointer to the static fields for {_staticFieldPtrObject?.TypeTheseFieldsAreFor.FullName} and stores it in {_constantMade?.Name} in register {_destReg}";
        }
    }
}