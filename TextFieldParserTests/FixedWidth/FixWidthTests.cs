﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TextFieldParserTests.FixedWidth;

[TestClass()]
public class FixWidthTests
{
    private const string WriteAttributeTestFile = "FixedWidthWriteAttributeTest.txt";
    private const string WriteConfigurationTestFile = "FixedWidthWriteConfigurationTest.txt";
    private const string ReadTestFile = "FixedWidthReadTest.txt";

    [TestMethod()]
    public void ReadWithAttributesTest()
    {
        //Arrange
        var actor = FileParseBuilder
            .AsFixedWidth<PersonWithAttributes>()
            .Build();

        //Act
        var actual = actor.ReadFile(ReadTestFile);

        //Assert
        Assert.AreEqual(actual.Count(), 7);
    }

    [TestMethod()]
    public void ReadWithConfigurationTest()
    {
        //Arrange
        var actor = FileParseBuilder
            .AsFixedWidth<Person>()
            .Configure(config =>
            {
                config
                .SetProperties(((1, 50), person => person.FirstName),
                     ((51, 50), person => person.LastName),
                     ((101, 3), person => person.Age));
            })
            .Build();

        //Act
        var actual = actor.ReadFile(ReadTestFile);

        //Assert
        Assert.AreEqual(actual.Count(), 7);
    }

    [TestMethod()]
    public void WriteWithConfigurationTest()
    {
        //Arrange
        var people = new List<Person>
        {
            new Person{ FirstName = "Mathew", LastName = "KJV", Age = "40" },
            new Person{ FirstName = "Mark", LastName = "KJV", Age = "40" },
            new Person{ FirstName = "Luke", LastName = "KJV", Age = "40" },
            new Person{ FirstName = "John", LastName = "KJV", Age = "40" },
            new Person{ FirstName = "Acts", LastName = "KJV", Age = "40" },
            new Person{ FirstName = "Romans", LastName = "KJV", Age = "40" },
            new Person{ FirstName = "Corinthians", LastName = "KJV", Age = "40" }
        };

        var actor = FileParseBuilder
            .AsFixedWidth<Person>()
            .Configure(config =>
            {
                config
                .SetProperties(
                    ((1, 50), person => person.FirstName),
                    ((51, 50), person => person.LastName),
                    ((101, 3), person => person.Age));
            })
            .Build();

        //Act
        actor.WriteFile(WriteConfigurationTestFile, people);

        //Assert
        Assert.IsTrue(File.Exists(WriteConfigurationTestFile));
        Assert.AreEqual(File.ReadLines(WriteConfigurationTestFile).Count(), 7);
    }

    [TestMethod()]
    public void WriteWithAttributeTest()
    {
        //Arrange
        var people = new List<PersonWithAttributes>
        {
            new PersonWithAttributes{ FirstName = "Mathew", LastName = "KJV", Age = "40" },
            new PersonWithAttributes{ FirstName = "Mark", LastName = "KJV", Age = "40" },
            new PersonWithAttributes{ FirstName = "Luke", LastName = "KJV", Age = "40" },
            new PersonWithAttributes{ FirstName = "John", LastName = "KJV", Age = "40" },
            new PersonWithAttributes{ FirstName = "Acts", LastName = "KJV", Age = "40" },
            new PersonWithAttributes{ FirstName = "Romans", LastName = "KJV", Age = "40" },
            new PersonWithAttributes{ FirstName = "Corinthians", LastName = "KJV", Age = "40" }
        };

        var actor = FileParseBuilder
            .AsFixedWidth<PersonWithAttributes>()
            .Build();

        //Act
        actor.WriteFile(WriteAttributeTestFile, people);

        //Assert
        Assert.IsTrue(File.Exists(WriteAttributeTestFile));
        Assert.AreEqual(File.ReadLines(WriteAttributeTestFile).Count(), 7);
    }
}
