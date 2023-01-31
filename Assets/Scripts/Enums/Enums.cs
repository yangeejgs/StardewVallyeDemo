public enum ToolEffect
{
    none,
    watering
}
public enum Direction
{
    up,
    down,
    left,
    right,
    none
}
public enum ItemType
{
    // 种子
    Seed,
    // 商品
    Commodity,
    // 浇水工具
    Wartering_tool,
    // 锄头工具
    Hoeing_tool,
    // 砍伐工具
    Choping_tool,
    // 敲打工具
    Breaking_tool,
    // 收割工具
    Reaping_tool,
    // 收集工具
    Collecting_tool,
    //可收集场景
    Reapable_scenary,
    // 家具
    Furniture,
    None,
    Count
}

public enum InventoryLocation
{
    // 玩家
    player,
    // 宝箱
    chest,
    // 数量
    count
}