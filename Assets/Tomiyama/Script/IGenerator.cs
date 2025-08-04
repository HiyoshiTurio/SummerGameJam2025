public interface IGenerator
{
    /// <summary>
    /// 食材を取得する処理。
    /// プレイヤーはこれを呼び出して食材を取得する。
    /// </summary>
    /// <param name="itemType"> 取得するアイテムのタイプ </param>
    /// <returns> 取得に成功したかどうか </returns>
    public bool TryTakeItem(out ItemType itemType);
}