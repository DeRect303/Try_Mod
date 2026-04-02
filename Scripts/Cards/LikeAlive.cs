using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using Try.Scripts.Powers;

namespace Try.Scripts.Cards;


[Pool(typeof(NecrobinderCardPool))]
public class LikeAlive : CustomCardModel
{
    private const int energycost = 1;

    private const CardType type = CardType.Skill;
    
    private const CardRarity rarity = CardRarity.Uncommon;

    private const TargetType target = TargetType.Self;
    
    private const bool shouldShowInCardLibrary = true;
    
    public LikeAlive() : base(energycost, type, rarity, target ,shouldShowInCardLibrary)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (this.IsUpgraded)
        {
            AlivePower alivePower = await PowerCmd.Apply<AlivePower>(this.Owner.Creature,1,this.Owner.Creature,(CardModel)this);
        }

        DoomPower doomPower = await PowerCmd.SetAmount<DoomPower>(this.Owner.Creature,0,this.Owner.Creature,(CardModel)this) ;
    }


}