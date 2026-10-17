using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage.Models;

namespace InfoResultTests
{
    [TestClass]
    public class RelatedObjectModelTests
    {
        [TestMethod]
        public void Ctor_InCodeNameAndDataSourceNames_AssignsBoth_Test()
        {
            var sut = new RelatedObjectModel("Book", "books", "book_v2");

            Assert.AreEqual("Book", sut.InCodeName);
            CollectionAssert.AreEqual(new[] { "books", "book_v2" }, sut.InDataSourceNames);
        }

        [TestMethod]
        public void Ctor_InCodeNameOnly_DataSourceNamesIsEmptyArray_Test()
        {
            var sut = new RelatedObjectModel("Book");

            Assert.AreEqual("Book", sut.InCodeName);
            Assert.IsNotNull(sut.InDataSourceNames);
            Assert.AreEqual(0, sut.InDataSourceNames.Length);
        }

        [TestMethod]
        public void Ctor_InCodeName_NullDataSourceNames_LeavesPropertyNull_Test()
        {
            var sut = new RelatedObjectModel("Book", (string[])null);

            Assert.AreEqual("Book", sut.InCodeName);
            Assert.IsNull(sut.InDataSourceNames);
            Assert.AreEqual("InCodeName: Book <-> InDataSourceName: ", sut.ToString());
        }

        [TestMethod]
        public void Ctor_DataSourceNamesOnly_InCodeNameIsNull_Test()
        {
            var sut = new RelatedObjectModel("books", "book_v2");

            Assert.AreEqual("books", sut.InCodeName);
            CollectionAssert.AreEqual(new[] { "book_v2" }, sut.InDataSourceNames);
        }

        [TestMethod]
        public void Ctor_Parameterless_ToString_DoesNotThrow_Test()
        {
            var sut = new RelatedObjectModel();

            Assert.IsNull(sut.InCodeName);
            Assert.IsNull(sut.InDataSourceNames);

            var text = sut.ToString();
            Assert.AreEqual("InCodeName:  <-> InDataSourceName: ", text);
        }

        [TestMethod]
        public void ToString_JoinsDataSourceNamesWithHash_Test()
        {
            var sut = new RelatedObjectModel("Book", "books", "book_v2", "book_v3");

            Assert.AreEqual(
                "InCodeName: Book <-> InDataSourceName: books#book_v2#book_v3",
                sut.ToString());
        }
    }
}