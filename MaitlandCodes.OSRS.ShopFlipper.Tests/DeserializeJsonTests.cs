using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MaitlandCodes.OSRS.ShopFlipper.Tests
{
    [TestClass]
    public class DeserializeJsonTests
    {
        [TestMethod]
        public async Task DeserializeIntoJsonElement()
        {
            var value = "2.1b";
            var element = JsonSerializer.SerializeToElement(value);
        }
    }
}
