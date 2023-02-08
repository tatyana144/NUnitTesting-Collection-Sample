using Collections;

namespace Collection.UnitTests
{
    public class CollectionTests
    {


        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            //Arrange and Act
            var coll = new Collection<int>();

            //Assert
            Assert.AreEqual(coll.ToString(), "[]");

            Assert.AreEqual(coll.Count, 0);
            Assert.AreEqual(coll.Capacity, 16);
        }

        [Test]
        public void Test_Collection_ConstuctorSingelItem()
        {
            //Arrange and Act
            var coll = new Collection<int>(5);

            //Assert
            Assert.AreEqual(coll.ToString(), "[5]");
        }

        [Test]
        public void Test_Collection_ConstuctorMultipleItem()
        {
            //Arrange and Act
            var coll = new Collection<int>(5, 6);

            //Assert
            Assert.AreEqual(coll.ToString(), "[5, 6]");
        }

        [Test]
        public void Test_Collection_CountAndCapacity()
        {
            //Arrange and Act
            var coll = new Collection<int>(5, 6);

            //Assert
            Assert.AreEqual(coll.Count, 2, "Check for count");
            Assert.That(coll.Capacity, Is.GreaterThan(coll.Count));
        }

        [Test]
        public void Test_Collection_Add()
        {
            //Arrange
            var coll = new Collection<string>("Vasil", "Ganka");

            //Act
            coll.Add("Georgi");
            //Assert
            Assert.AreEqual(coll.ToString(), "[Vasil, Ganka, Georgi]");
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            //Arrange
            var coll = new Collection<int>(5, 6, 7);

            //Act
            var item = coll[1];

            //Assert
            Assert.That(item.ToString(), Is.EqualTo("6"));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            //Arrange
            var coll = new Collection<int>(5, 6, 7);

            //Act
            coll[1] = 666;

            //Assert
            Assert.That(coll.ToString(), Is.EqualTo("[5, 666, 7]"));
        }

        [Test]
        public void Test_Collection_SetByInvalidIndex()
        {
            //Arrange
            var coll = new Collection<int>(5, 6, 7, 8);

            //Act


            //Assert
            Assert.That(() => { var item = coll[4]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            //Arrange
            var coll = new Collection<string>("Vasil", "Dayana");

            //Act


            //Assert
            Assert.That(() => { var item = coll[2]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_AddWithGrow()
        {
            //Arrange
            var coll = new Collection<int>();
            int oldCapacity = coll.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();

            //Act
            coll.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";

            //Assert
            Assert.That(coll.ToString(), Is.EqualTo(expectedNums));
            Assert.That(coll.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(coll.Capacity, Is.GreaterThanOrEqualTo(coll.Count));
        }

        [Test]
        public void Test_Collection_ToStringNestedColections()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Deyan");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();

            //Act
            var nested = new Collection<object>(names, nums, dates);
            string nestedToString = nested.ToString();

            //Assert
            Assert.That(nestedToString,
                Is.EqualTo("[[Ivan, Deyan], [10, 20], []]"));
        }

        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MillionItems()
        {
            //Arrange
            const int itemsCount = 1000000;
            var nums = new Collection<int>();

            //Act
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());

            //Assert
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);

            //Act
            for (int i = itemsCount - 1; i >= 0; i--)
            {
                nums.RemoveAt(i);
            }

            //Assert
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

        [Test]
        public void Test_Collection_InsertAtStart()
        {
            //Arrange
            var coll = new Collection<int>(10, 12, 14);

            //Act
            coll.InsertAt(0, 8);

            //Assert
            Assert.That(coll[0], Is.EqualTo(8));
        }

        [Test]
        public void Test_Collection_InsertAtEnd()
        {
            //Arrange
            var coll = new Collection<int>(10, 12, 14);

            //Act
            coll.InsertAt(3, 16);

            //Assert
            Assert.That(coll[3], Is.EqualTo(16));
        }

        [Test]
        public void Test_Collection_InsertAtMiddle()
        {
            //Arrange
            var coll = new Collection<int>(10, 12, 14, 16);

            //Act
            coll.InsertAt(2, 13);

            //Assert
            Assert.That(coll[2], Is.EqualTo(13));
        }

        [Test]
        public void Test_Collection_ExchangeMiddle()
        {
            //Arrange
            var coll = new Collection<int>(10, 15, 20, 25);

            //Act
            coll.Exchange(1, 2);

            //Assert
            Assert.That(coll[1], Is.EqualTo(20));
            Assert.That(coll[2], Is.EqualTo(15));
        }

        [Test]
        public void Test_Collection_ExchangeFirstLaste()
        {
            //Arrange
            var coll = new Collection<int>(10, 15, 20);

            //Act
            coll.Exchange(0, 2);

            //Assert
            Assert.That(coll[0], Is.EqualTo(20));
            Assert.That(coll[2], Is.EqualTo(10));
        }

        [Test]
        public void Test_Collection_Clear()
        {
            //Arrange
            var coll = new Collection<int>(1, 5, 10, 15);

            //act
            coll.Clear();

            //Assert
            Assert.That(coll, Is.Empty);
        }

        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            //Arrange
            var coll = new Collection<string>("Vasil", "Deyan", "Valentin");

            //Act
            coll.RemoveAt(0);
            string result = coll.ToString();

            //Assert
            Assert.That(result, Is.EqualTo("[Deyan, Valentin]"));
        }

        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            //Arrange
            var coll = new Collection<string>("Vasil", "Deyan", "Valentin");

            //Act
            coll.RemoveAt(2);
            string result = coll.ToString();

            //Assert
            Assert.That(result, Is.EqualTo("[Vasil, Deyan]"));
        }

        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            //Arrange
            var coll = new Collection<string>("Vasil", "Deyan", "Valentin");

            //Act
            coll.RemoveAt(1);
            string result = coll.ToString();

            //Assert
            Assert.That(result, Is.EqualTo("[Vasil, Valentin]"));
        }

        [TestCase("Vasil,Deyan,Valentin", 0, "Vasil")]
        [TestCase("Vasil,Deyan,Valentin", 2, "Valentin")]
        public void Test_Collection_GetByValidIndex(string data, int index, string expected)
        {
            //Act
            var coll = new Collection<string>(data.Split(","));
            var actual = coll[index];

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

      
    }
}