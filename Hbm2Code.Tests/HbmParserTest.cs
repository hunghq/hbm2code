using FluentAssertions;
using Hbm2Code.Tests.Utils;
using System.Collections.Generic;
using Xunit;

namespace Hbm2Code.Tests
{
    public class HbmParserTest
    {
        [Fact]
        public void ParseRootClass()
        {
            IList<ClassInfo> clazzInfos = TestUtils.ParseHbm("BaseObject.hbm.xml");
            var clazz = clazzInfos.AssertHasClass("BaseObject", ClassType.RootClass);

            clazz.OwnProperty.Should()
                .HaveTagName("class")
                .HaveAttribute("abstract", "true")
                .HaveAttribute("table", "BaseObject");
        }

        [Fact]
        public void ParseJoinedSubClass()
        {
            IList<ClassInfo> clazzInfos = TestUtils.ParseHbm("Agency.hbm.xml");
            var clazz = clazzInfos.AssertHasClass("Agency", ClassType.JoinedSubClass);

            clazz.OwnProperty.Should()
                .HaveTagName("joined-subclass")
                .HaveAttribute("table", "Agency")
                .NotHaveAttribute("extends");
        }

        [Fact]
        public void ParseRootClass_WithDiscriminatorColumn()
        {
            IList<ClassInfo> clazzInfos = TestUtils.ParseHbm("Worker.hbm.xml");
            var worker = clazzInfos.AssertHasClass("Worker", ClassType.RootClass);
            Property discriminator = worker.GetDiscriminator();
            discriminator.Should().NotBeNull();
            discriminator.Should()
                .HaveAttribute("column", "Type");
        }

        [Fact]
        public void ParseSubClass_WithDiscriminatorValue()
        {
            IList<ClassInfo> clazzInfos = TestUtils.ParseHbm("Worker.hbm.xml");

            var foreignWorker = clazzInfos.AssertHasClass("ForeignWorker", ClassType.SubClass);
            foreignWorker.OwnProperty.Should()
                .HaveAttribute("discriminator-value", "FW")
                .NotHaveAttribute("table");
        }

        [Fact]
        public void ParseSubClass_InsideRootClass()
        {
            IList<ClassInfo> clazzInfos = TestUtils.ParseHbm("Worker.hbm.xml");
            var domesticWorker = clazzInfos.AssertHasClass("DomesticWorker", ClassType.SubClass);
            domesticWorker.OwnProperty.Should()
                .HaveAttribute("discriminator-value", "DW")
                .NotHaveAttribute("table");
        }
    }
}