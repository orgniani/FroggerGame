using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Player;

public class PlayerViewTests
{
    private GameObject _go;
    private PlayerView _view;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        _go = new GameObject("PlayerViewTestObject");
        _view = _go.AddComponent<PlayerView>();
        _animator = _go.AddComponent<Animator>();
        _spriteRenderer = _go.AddComponent<SpriteRenderer>();

        _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AC_PlayerMock");

        var flags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
        typeof(PlayerView).GetField("animator", flags)?.SetValue(_view, _animator);
        typeof(PlayerView).GetField("spriteRenderer", flags)?.SetValue(_view, _spriteRenderer);
        typeof(PlayerView).GetField("moveCurve", flags)?.SetValue(_view, AnimationCurve.EaseInOut(0, 0, 1, 1));

        yield return null;
    }

    [UnityTest]
    public IEnumerator UpdatePosition_MovesSmoothlyToTarget()
    {
        Vector3 targetPos = new Vector3(2f, 3f, 0f);
        _view.UpdatePosition(targetPos.x, targetPos.y);

        yield return new WaitForSeconds(0.4f);

        Assert.That(Vector3.Distance(_view.transform.position, targetPos), Is.LessThan(0.1f));
    }

    [UnityTest]
    public IEnumerator SetFacingDirection_FlipsSprite()
    {
        _view.SetFacingDirection(-1);
        yield return null;

        Assert.IsTrue(_spriteRenderer.flipX);

        _view.SetFacingDirection(1);
        yield return null;

        Assert.IsFalse(_spriteRenderer.flipX);
    }

    [UnityTest]
    public IEnumerator ResetPosition_SetsInitialY()
    {
        _view.transform.position = new Vector3(1f, 1f, 0f);

        typeof(PlayerView).GetField("_startingY", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(_view, 0f);

        _view.ResetPosition();
        yield return null;

        Assert.AreEqual(0f, _view.transform.position.y);
        Assert.AreEqual(0f, _view.transform.position.x);
    }

    [UnityTest]
    public IEnumerator PlayGameOverAnimation_SetsDeadBool()
    {
        _view.PlayGameOverAnimation(true);
        yield return null;

        Assert.IsTrue(_animator.GetBool("dead"));

        _view.PlayGameOverAnimation(false);
        yield return null;

        Assert.IsFalse(_animator.GetBool("dead"));
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(_go);
        yield return null;
    }
}