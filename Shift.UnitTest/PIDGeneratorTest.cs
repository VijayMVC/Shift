﻿using System;
using Xunit;
using System.Threading;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace Shift.UnitTest
{
     
    public class PIDGeneratorTest
    {

        [Fact]
        public void GenerateProcessIDNotUseExisting()
        {
            var newPID = ProcessIDGenerator.Generate(false);

            Guid outGuid;
            var isValid = Guid.TryParse(newPID, out outGuid);

            Assert.True(isValid);
            Assert.NotNull(outGuid);
            Assert.Equal(newPID, outGuid.ToString("N").ToUpper());
        }


        [Fact]
        public void GenerateProcessIDUseExisting()
        {
            ProcessIDGenerator.DeleteExistingProcessID(); //ensures generating new PID
            var newPID = ProcessIDGenerator.Generate(true);
            var existingPID = ProcessIDGenerator.Generate(true); //should return the existing PID

            Assert.NotNull(newPID);
            Assert.NotNull(existingPID);
            Assert.Equal(newPID, existingPID);
        }

        [Fact]
        public async Task GenerateAsyncProcessIDNotUseExisting()
        {
            var newPID = await ProcessIDGenerator.GenerateAsync(false);

            Guid outGuid;
            var isValid = Guid.TryParse(newPID, out outGuid);

            Assert.True(isValid);
            Assert.NotNull(outGuid);
            Assert.Equal(newPID, outGuid.ToString("N").ToUpper());
        }


        [Fact]
        public async Task GenerateAsyncProcessIDUseExisting()
        {
            ProcessIDGenerator.DeleteExistingProcessID(); //ensures generating new PID
            var newPID = await ProcessIDGenerator.GenerateAsync(true);
            var existingPID = await ProcessIDGenerator.GenerateAsync(true); //should return the existing PID

            Assert.NotNull(newPID);
            Assert.NotNull(existingPID);
            Assert.Equal(newPID, existingPID);
        }


    }
}
