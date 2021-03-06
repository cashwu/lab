﻿using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyGroupByTests
    {
        [Test]
        public void groupBy_lastName()
        {
            var employees = new List<Employee>
            {
                new Employee { FirstName = "Joey", LastName = "Chen" },
                new Employee { FirstName = "Tom", LastName = "Lee" },
                new Employee { FirstName = "Eric", LastName = "Chen" },
                new Employee { FirstName = "John", LastName = "Chen" },
                new Employee { FirstName = "David", LastName = "Lee" },
            };

            var actual = JoeyGroupBy(employees, current => current.LastName);
            Assert.AreEqual(2, actual.Count());

            var firstGroup = new List<Employee>
            {
                new Employee { FirstName = "Joey", LastName = "Chen" },
                new Employee { FirstName = "Eric", LastName = "Chen" },
                new Employee { FirstName = "John", LastName = "Chen" },
            };

            firstGroup.ToExpectedObject().ShouldMatch(actual.First().ToList());
        }

        private IEnumerable<IGrouping<string, Employee>> JoeyGroupBy(IEnumerable<Employee> employees, Func<Employee, string> groupKeySelector)
        {
            var lookup = new MyLookup();

            foreach (var employee in employees)
            {
                lookup.AddElement(groupKeySelector(employee), employee);
            }

            return lookup;
        }
    }
}