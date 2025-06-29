using UnityEngine;

public class window : InteractableObject
{
    public Transform pointA;
    public Transform pointB;
    public override void Interact(GameObject interactor)
    {
        base.Interact(interactor);

        PlayerAction playerAction = interactor.GetComponent<PlayerAction>();
        Vector3 from = interactor.transform.position;
        Vector3 startPos, toPos;
        if (Vector3.Distance(from, pointA.position) < Vector3.Distance(from, pointB.position))
        {
            startPos = pointA.position;
            toPos = pointB.position;
        }
        else
        {
            startPos = pointB.position;
            toPos = pointA.position;
        }

        // 先把玩家放到起點
        interactor.transform.position = startPos;

        // 計算朝向向量，只留水平分量
        Vector3 dir = (toPos - startPos).normalized;
        dir.y = 0f;

        // 轉向：讓 forward 朝向窗戶另一側
        interactor.transform.rotation = Quaternion.LookRotation(dir);

        // 再開始跨越
        playerAction.CrossWindow();
    }
}
