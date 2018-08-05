﻿#if !NO_RUNTIME
using System;

namespace ProtoBuf.Serializers
{
    internal sealed class GuidSerializer : IProtoSerializer
    {
        static readonly Type expectedType = typeof(Guid);

        public Type ExpectedType { get { return expectedType; } }

        bool IProtoSerializer.RequiresOldValue => false;

        bool IProtoSerializer.ReturnsValue => true;

        public void Write(object value, ProtoWriter dest)
        {
            BclHelpers.WriteGuid((Guid)value, dest);
        }

        public object Read(ref ProtoReader.State state, object value, ProtoReader source)
        {
            Helpers.DebugAssert(value == null); // since replaces
            return BclHelpers.ReadGuid(ref state, source);
        }

#if FEAT_COMPILER
        void IProtoSerializer.EmitWrite(Compiler.CompilerContext ctx, Compiler.Local valueFrom)
        {
            ctx.EmitWrite(ctx.MapType(typeof(BclHelpers)), "WriteGuid", valueFrom);
        }

        void IProtoSerializer.EmitRead(Compiler.CompilerContext ctx, Compiler.Local valueFrom)
        {
            ctx.EmitBasicRead(ctx.MapType(typeof(BclHelpers)), "ReadGuid", ExpectedType);
        }
#endif

    }
}
#endif