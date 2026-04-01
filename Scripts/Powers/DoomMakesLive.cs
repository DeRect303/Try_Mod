using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Try.Scripts.Powers;

public class DoomMakesLivePower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    
    public override decimal ModifyHpLostAfterOstyLate(
        Creature target,
        decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        // 1. 获取 DoomPower 实例（而不是直接拿 Amount，方便后续操作）
        var doomPower = this.Owner.GetPower<DoomPower>();

        // 2. 空引用检查
        if (doomPower != null && doomPower.Amount > 0)
        {
            // 3. 逻辑：如果伤害小于 Doom 层数，则用 Doom 抵扣，伤害变为 0
            if (amount <= doomPower.Amount)
            {
                doomPower.Amount -= (int)Math.Round(amount); // 减少 Doom 层数
                return 0m; // 返回 0 伤害
            }
            else
            {
                // 如果伤害超过了 Doom，扣光 Doom，剩余伤害继续
                decimal remainingDamage = amount - doomPower.Amount;
                doomPower.Amount = 0;
                return remainingDamage;
            }
        }

        return amount; // 默认返回原伤害
    }
    
    
}