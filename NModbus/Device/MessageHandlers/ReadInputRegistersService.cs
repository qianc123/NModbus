﻿using NModbus.Data;
using NModbus.Interfaces;
using NModbus.Message;

namespace NModbus.Device.MessageHandlers
{
    internal class ReadInputRegistersService : ModbusFunctionServiceBase<ReadHoldingInputRegistersRequest>
    {
        public ReadInputRegistersService() 
            : base(ModbusFunctionCodes.ReadInputRegisters)
        {
        }

        public override int GetRtuRequestBytesToRead(byte[] frameStart)
        {
            return 1;
        }

        public override int GetRtuResponseBytesToRead(byte[] frameStart)
        {
            return frameStart[2] + 1;
        }

        protected override IModbusMessage Handle(ReadHoldingInputRegistersRequest request, ISlaveDataStore dataStore)
        {
            ushort[] registers = dataStore.InputRegisters.ReadPoints(request.StartAddress, request.NumberOfPoints);

            RegisterCollection regsiterCollection = new RegisterCollection(registers);

            return new ReadHoldingInputRegistersResponse(request.FunctionCode, request.SlaveAddress, regsiterCollection);
        }
    }
}