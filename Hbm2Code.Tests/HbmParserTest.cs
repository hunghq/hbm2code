﻿using FluentAssertions;
using Hbm2Code.Tests.Utils;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hbm2Code.Tests
{
    public class HbmParserTest
    {
        [Fact]
        public void ParseAbstractRootClass()
        {
            ClassInfo clazz = TestUtils.ParseHbm("BaseObject.hbm.xml", "BaseObject", ClassType.RootClass);

            clazz.OwnProperty.Should()
                .HaveTagName("class")
                .HaveAttribute("abstract", "true")
                .HaveAttribute("table", "BaseObject");
        }

        [Fact]
        public void ParseJoinedSubClass()
        {
            ClassInfo clazz = TestUtils.ParseHbm("Agency.hbm.xml", "Agency", ClassType.JoinedSubClass);

            clazz.OwnProperty.Should()
                .HaveTagName("joined-subclass")
                .HaveAttribute("table", "Agency")
                .NotHaveAttribute("extends");
        }

        [Fact]
        public void ParseRootClass_WithDiscriminatorColumn()
        {
            ClassInfo worker = TestUtils.ParseHbm("Worker.hbm.xml", "Worker", ClassType.RootClass);

            Property discriminator = worker.GetDiscriminator();
            discriminator.Should().NotBeNull();
            discriminator.Should()
                .HaveAttribute("column", "Type");
        }

        [Fact]
        public void ParseSubClass_WithDiscriminatorValue()
        {
            ClassInfo foreignWorker = TestUtils.ParseHbm("Worker.hbm.xml", "ForeignWorker", ClassType.SubClass);

            foreignWorker.OwnProperty.Should()
                .HaveAttribute("discriminator-value", "FW")
                .NotHaveAttribute("table");
        }

        [Fact]
        public void ParseSubClass_InsideRootClass()
        {
            ClassInfo domesticWorker = TestUtils.ParseHbm("Worker.hbm.xml", "DomesticWorker", ClassType.SubClass);

            domesticWorker.OwnProperty.Should()
                .HaveAttribute("discriminator-value", "DW")
                .NotHaveAttribute("table");
        }

        [Fact]
        public void ParseManyToOne()
        {
            ClassInfo agency = TestUtils.ParseHbm("Agency.hbm.xml", "Agency", ClassType.JoinedSubClass);
            var areaProperty = agency.GetManyToOneProperties().GetAndAssertProperty("Area", agency.ClassName);

            areaProperty.Should()
                .HaveName("Area")
                .HaveTagName("many-to-one")
                .HaveAttribute("column", "AreaId")
                .HaveAttribute("lazy", "proxy")
                .HaveAttribute("not-null", "true")
                .HaveAttribute("foreign-key", "FK_Agency_Area");
        }

        [Fact]
        public void ParseOneToOne()
        {
            ClassInfo headquarter = TestUtils.ParseHbm("Agency.hbm.xml", "HeadQuarter", ClassType.RootClass);
            Property agencyProperty = headquarter.GetOneToOneProperties().GetAndAssertProperty("Agency", headquarter.ClassName);

            agencyProperty.Should()
                .HaveName("Agency")
                .HaveTagName("one-to-one")
                .HaveAttribute("constrained", "true")
                .HaveAttribute("foreign-key", "FK_Agency_HeadQuarter");
        }

        [Fact]
        public void ParseKeyProperty()
        {
            ClassInfo agency = TestUtils.ParseHbm("Agency.hbm.xml", "Agency", ClassType.JoinedSubClass);
            Property keyProperty = agency.GetKeyProperty();

            keyProperty.Should().NotBeNull();
            keyProperty.Should()
                .HaveName(null)
                .HaveTagName("key")
                .HaveAttribute("column", "Id")
                .HaveAttribute("foreign-key", "FK_Application");
        }

        [Fact]
        public void ParseIdProperty_AssignedId()
        {
            ClassInfo clazz = TestUtils.ParseHbm("BaseObject.hbm.xml", "BaseObject", ClassType.RootClass);
            IdProperty idProperty = clazz.GetIdProperty();

            idProperty.Should().NotBeNull();
            idProperty.Should()
                .HaveName("Id")
                .HaveTagName("Id")
                .HaveAttribute("type", "Int64")
                .HaveAttribute("generator", "assigned")
                .NotHaveAttribute("name");

            idProperty.GeneratorParams.Should().NotBeNull();

            idProperty.GeneratorParams.Should()
                .HaveName("assigned")
                .HaveTagName("generator");

            idProperty.GeneratorParams.Should().BeEmpty();
        }

        [Fact]
        public void ParseIdProperty_ForeignColumn()
        {
            ClassInfo clazz = TestUtils.ParseHbm("Agency.hbm.xml", "HeadQuarter", ClassType.RootClass);
            IdProperty idProperty = clazz.GetIdProperty();

            idProperty.Should()
                .HaveName("Id")
                .HaveTagName("Id")
                .HaveAttribute("type", "Int64")
                .HaveAttribute("generator", "foreign");

            idProperty.GeneratorParams.Should()
                .HaveAttribute("property", "Agency");
        }

        [Fact]
        public void ParseOneToManySet()
        {
            var clazz = TestUtils.ParseHbm("Area.hbm.xml", "Area", ClassType.RootClass);

            clazz.GetSets().Should().HaveCount(1);
            Collection set = clazz.GetSets().Single();

            set.Should()
                .HaveName("Agencies")
                .HaveAttribute("access", "property")
                .HaveAttribute("lazy", "true")
                .HaveAttribute("inverse", "true")
                .HaveAttribute("cascade", "all")
                .HaveAttribute("batch-size", "20");

            set.KeyProperty.Should()
                .HaveName(null)
                .HaveTagName("key")
                .HaveAttribute("column", "AreaId");

            set.RelationProperty.Should()
                .HaveName(null)
                .HaveTagName("one-to-many")
                .HaveAttribute("class", "Agency");

            set.ExtendedPropertySets.Should().HaveCount(1);
            set.ExtendedPropertySets.Single().Should().HaveCount(1);
            set.ExtendedPropertySets.Single().Single().Should().BeSameAs(set.RelationProperty);
        }

        [Fact]
        public void ParseManyToManyBag()
        {
            var clazz = TestUtils.ParseHbm("Category.hbm.xml", "Category", ClassType.RootClass);

            clazz.GetBags().Should().HaveCount(1);
            Collection bag = clazz.GetBags().Single();

            bag.Should()
                .HaveName("Items")
                .HaveAttribute("table", "CategoryItem");

            bag.KeyProperty.Should()
                .HaveAttribute("column", "CategoryId");

            bag.RelationProperty.Should()
                .HaveTagName("many-to-many")
                .HaveAttribute("class", "Item")
                .HaveAttribute("column", "ItemId");
        }
    }
}