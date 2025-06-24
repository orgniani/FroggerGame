using NUnit.Framework;
using Player;
using UnityEngine;

namespace Tests.Editor.Player
{
    public class PlayerInputHandlerTests
    {
        private PlayerInputHandler _inputHandler;

        [SetUp]
        public void Setup()
        {
            _inputHandler = new PlayerInputHandler(0.5f);
        }

        [Test]
        public void GetMoveDirection_UpInput_ReturnsVector2IntUp()
        {
            Vector2 input = new Vector2(0f, 1f);
            Vector2Int result = _inputHandler.GetMoveDirection(input);
            Assert.AreEqual(Vector2Int.up, result);
        }

        [Test]
        public void GetMoveDirection_DownInput_ReturnsVector2IntDown()
        {
            Vector2 input = new Vector2(0f, -1f);
            Vector2Int result = _inputHandler.GetMoveDirection(input);
            Assert.AreEqual(Vector2Int.down, result);
        }

        [Test]
        public void GetMoveDirection_RightInput_ReturnsVector2IntRight()
        {
            Vector2 input = new Vector2(1f, 0f);
            Vector2Int result = _inputHandler.GetMoveDirection(input);
            Assert.AreEqual(Vector2Int.right, result);
        }

        [Test]
        public void GetMoveDirection_LeftInput_ReturnsVector2IntLeft()
        {
            Vector2 input = new Vector2(-1f, 0f);
            Vector2Int result = _inputHandler.GetMoveDirection(input);
            Assert.AreEqual(Vector2Int.left, result);
        }

        [Test]
        public void GetMoveDirection_InputWithinThreshold_ReturnsZero()
        {
            Vector2 input = new Vector2(0.1f, -0.1f);
            Vector2Int result = _inputHandler.GetMoveDirection(input);
            Assert.AreEqual(Vector2Int.zero, result);
        }

        [Test]
        public void GetMoveDirection_InputEqualsThreshold_ReturnsZero()
        {
            Vector2 input = new Vector2(0.5f, 0.5f);
            Vector2Int result = _inputHandler.GetMoveDirection(input);
            Assert.AreEqual(Vector2Int.zero, result);
        }

        [Test]
        public void GetMoveDirection_DiagonalInputAboveThreshold_ReturnsDiagonalVector()
        {
            Vector2 input = new Vector2(1f, 1f);
            Vector2Int result = _inputHandler.GetMoveDirection(input);
            Assert.AreEqual(new Vector2Int(1, 1), result);
        }

        [Test]
        public void GetMoveDirection_InputEqualsNegativeThreshold_ReturnsZero()
        {
            Vector2 input = new Vector2(-0.5f, -0.5f);
            Vector2Int result = _inputHandler.GetMoveDirection(input);
            Assert.AreEqual(Vector2Int.zero, result);
        }
    }
}