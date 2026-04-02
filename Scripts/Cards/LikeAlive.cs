using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace Try.Scripts.Cards;


[Pool(typeof(NecrobinderCardPool))]
public class LikeAlive : CustomCardModel
{
    private const int energycost = 1;

    private const CardType type = CardType.Skill;
    
    private const CardRarity rarity = CardRarity.Common;

    private const TargetType target = TargetType.Self;
    
    private const bool shouldShowInCardLibrary = true;
    
    public LikeAlive() : base(energycost, type, rarity, target ,shouldShowInCardLibrary)
    {
    }
}