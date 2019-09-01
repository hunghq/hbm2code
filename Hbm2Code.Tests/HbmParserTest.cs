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
                .NotHaveAttribute("table");
        }

        [Fact]
        public void ParseJoinedSubClass()
        {
            IList<ClassInfo> clazzInfos = TestUtils.ParseHbm("Agency.hbm.xml");
            var clazz = clazzInfos.AssertHasClass("Agency", ClassType.JoinedSubClass);

            clazz.OwnProperty.Should()
                .HaveTagName("joined-subclass")
                .NotHaveAttribute("table")
                .NotHaveAttribute("extends");
        }

        [Fact]
        public void ParseSubClass_WithDiscriminatorValue()
        {
            IList<ClassInfo> clazzInfos = TestUtils.ParseHbm("Worker.hbm.xml");

            var foreignWorker = clazzInfos.AssertHasClass("ForeignWorker", ClassType.SubClass);
            foreignWorker.OwnProperty.Should().HaveAttribute("discriminator-value", "FW");
        }

        [Fact]
        public void ParseSubClass_InsideRootClass()
        {
            IList<ClassInfo> clazzInfos = TestUtils.ParseHbm("Worker.hbm.xml");
            var domesticWorker = clazzInfos.AssertHasClass("DomesticWorker", ClassType.SubClass);
            domesticWorker.OwnProperty.Should().HaveAttribute("discriminator-value", "DW");
        }
    }
}