using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyUpdater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyUpdater.Tests
{
    [TestClass()]
    public class CurrencyUpdateTests
    {
        [TestMethod()]
        public void GetCurrencyTest()
        {
             string  val = "GBP";

            CurrencyUpdate currencyUpdate = new CurrencyUpdate();

            var result = currencyUpdate.GetCurrency(val);

            Assert.AreEqual(result, 97.9490);
        }
    }
}