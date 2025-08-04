public interface ICustomer
{
    /// <summary>
    /// 要求を受け取る処理。プレイヤーはこれを提供時に呼び出す。
    /// </summary>
    /// <returns>待たせた時間に応じたスコアを返す</returns>
    public bool Take(ItemType itemType);
}