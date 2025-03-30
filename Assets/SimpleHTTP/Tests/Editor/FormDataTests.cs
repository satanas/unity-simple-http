using System.Text;
using NUnit.Framework;

namespace SimpleHTTP.Tests.Editor {
    public class FormDataTests {
        // Subject under test
        private FormData formData;

        [Test]
        public void TestAddField() {
            // Execution
            formData = new FormData();
            formData.AddField("my_section", "my_value");

            // Expected
            Assert.AreEqual(1, formData.MultipartForm().Count);
            Assert.AreEqual("my_section", formData.MultipartForm()[0].sectionName);
            Assert.AreEqual("my_value", Encoding.UTF8.GetString(formData.MultipartForm()[0].sectionData));
        }

        [Test]
        public void TestAddFileWithDefaultValues() {
            // Setup
            byte[] data = Encoding.UTF8.GetBytes("my_data");

            // Execution
            formData = new FormData();
            formData.AddFile("file_name", data);

            // Expected
            Assert.AreEqual(1, formData.MultipartForm().Count);
            Assert.AreEqual("file_name", formData.MultipartForm()[0].fileName);
            Assert.AreEqual(data, formData.MultipartForm()[0].sectionData);
            Assert.AreEqual("application/octet-stream", formData.MultipartForm()[0].contentType);
        }

        [Test]
        public void TestAddFileWithContentType() {
            // Setup
            byte[] data = Encoding.UTF8.GetBytes("my_data");

            // Execution
            formData = new FormData();
            formData.AddFile("my_section", data, "file_name", "multipart/form-data");

            // Expected
            Assert.AreEqual(1, formData.MultipartForm().Count);
            Assert.AreEqual("my_section", formData.MultipartForm()[0].sectionName);
            Assert.AreEqual("file_name", formData.MultipartForm()[0].fileName);
            Assert.AreEqual(data, formData.MultipartForm()[0].sectionData);
            Assert.AreEqual("multipart/form-data", formData.MultipartForm()[0].contentType);
        }
    }
}
