/*  Name: Reily Stanford
 *  Date: 11/21/2020
 *  File: BankTest.cs
 *  Description:
 *      This file is for testing the features of the Bank program 
 *      to make sure that exceptions are properly thrown when an error occurs.
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;

namespace BankTest
{
    [TestClass]
    public class BankTest
    {
        // unit test code  
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act  
            account.Debit(debitAmount);

            // assert  
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act  
            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert  
                StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
                return;
            }
            Assert.Fail("No exception was thrown.");

            // assert is handled by ExpectedException  
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            double beginningBalance = 11.99;
            double debitAmount = 13.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            //act
            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert  
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Credit_WhenAccountIsFrozen_ShouldThrowException()
        {
            double beginningBalance = 20.00;
            double creditAmount = 5.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            account.ToggleFreeze();

            account.Credit(creditAmount);
        }

        [TestMethod]
        public void Credit_WhenAmountIsNegative_ShouldThrowOutOfRangeException()
        {
            //arrange
            double beginningBalance = 20.00;
            double creditAmount = -5.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            //act
            try
            {
                account.Credit(creditAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert  
                StringAssert.Contains(e.Message, BankAccount.CreditAmountLessThanZeroMessage);
                return;
            }
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        public void Credit_WithValidAmount_UpdatesBalance()
        {
            // arrange  
            double beginningBalance = 12.00;
            double creditAmount = 4.49;
            double expected = 16.49;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act  
            account.Credit(creditAmount);

            // assert  
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not credited correctly");
        }

    }
}
