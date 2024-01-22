using Stealthy.Model;
using Stealthy.Persistence;
using Moq;

namespace StealthyTest
{
    [TestClass]
    public class UnitTest1
    {
        private StealthyGameModel _model = null!;
        private Player _player = null!;
        private StealthyTable _mockedTable = null!; // mockolt játéktábla
        private Mock<IStealthyDataAcces> _mock = null!; // az adatelérés mock-ja

        [TestInitialize]
        public void TestInitialize()
        {
            _mockedTable = new StealthyTable(6);
            for(int i = 0; i < 6; ++i)
            {
                _mockedTable.FieldValue(i, 0, FieldElement.WALL);
                _mockedTable.FieldValue(0,i,FieldElement.WALL);
                _mockedTable.FieldValue(5,i,FieldElement.WALL);
                _mockedTable.FieldValue(i, 5, FieldElement.WALL);
            }
            for(int i=1;i < 5; ++i)
            {
                for(int j=1;j < 5; ++j)
                {
                    _mockedTable.FieldValue(i, j, FieldElement.FLOOR);
                }
            }
            _mock = new Mock<IStealthyDataAcces>();
            _mock.Setup(mock => mock.Load(It.IsAny<String>()))
                .Returns(() => _mockedTable);

            _model = new StealthyGameModel(_mock.Object);
        }

        [TestMethod]
        public void TestMethod1()
        {
            _model.NewGame();
            Assert.AreEqual(GameStatus.START, _model.GetGameStatus );
        }
        [TestMethod]
        public void TestMethod2()
        {
            _mockedTable.FieldValue(2, 3, FieldElement.PLAYER);
            _mockedTable.FieldValue(4, 4, FieldElement.GUARD);
            Assert.AreEqual(_mockedTable.GetField(1, 1), FieldElement.FLOOR);
            Assert.AreEqual(_mockedTable.GetField(0, 4), FieldElement.WALL);
            Assert.AreEqual(_mockedTable.GetField(3, 3), FieldElement.FLOOR);
            Assert.AreEqual(_mockedTable.GetField(2, 3), FieldElement.PLAYER);
            Assert.AreEqual(_mockedTable.GetField(4, 4), FieldElement.GUARD);
        }
        [TestMethod]
        public void TestMethod3()
        {
            _mockedTable.FieldValue(2, 3, FieldElement.PLAYER);
            _player = new Player(_model, 2, 3);
            Assert.AreEqual(_player.PlayerX(), 2);
            Assert.AreEqual(_player.PlayerY(), 3);
        }
        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(GameStatus.START, _model.GetGameStatus);
            Assert.AreEqual(GameTable.LARGE, _model.gameTable);
        }
    }
}