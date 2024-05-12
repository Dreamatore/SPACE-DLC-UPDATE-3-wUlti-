using System;

namespace Base;
public class Program
{
    private static void Main(string[] args)
    {

        var ship1 = new Критовик();
        var ship2 = new Танк();
        var ship3 = new Ловкач();
        var arbiter = new FightArbiter(ship1, ship2, ship3);
        arbiter.Fight();
        Random rand = new Random();

    }
    public class FightArbiter
    {
        private readonly BaseShip _first;
        private readonly BaseShip _second;
        private readonly BaseShip _third;


        public FightArbiter(BaseShip first, BaseShip second, BaseShip third)
        {
            _first = first;
            _second = second;
            _third = third;
        }
                private (bool vision, bool fight) AttackTick(BaseShip a, BaseShip b, BaseShip c, bool discovery)
                {
        if      (!discovery)
                {
                var vision = a.VisionRange >= (b.VisibilityRange - c.VisibilityRange + a.VisibilityRange);
        if      (!vision)
                {
                _first.Move();
                _second.Move();
                _third.Move();
                return (false, false);
                }
                }
                var InstaKillAB = a.Damage > (b.Shield + b.HP);
                var InstaKillAC = a.Damage > (c.Shield + c.HP);
                var InstaKillCA = c.Damage > (a.Shield + a.HP);
                var InstaKillCB = c.Damage > (b.Shield + b.HP);
                var InstaKillBC = b.Damage > (c.Shield + c.HP);
                var InstaKillBA = b.Damage > (a.Shield + a.HP);
        if     (InstaKillAB)
                {
                b.HP = 0;              
                Console.WriteLine($"{a.Name} winning , {b.Name} destroyed {c.Name} is alive");
                return (true, true);
                }
        if (InstaKillAC)
                {
                c.HP = 0;
                Console.WriteLine($"{a.Name} winning , {c.Name} destroyed {b.Name} is alive");
                return (true, true);
                }
        if (InstaKillCA)
                {
                a.HP = 0;
                Console.WriteLine($"{c.Name} winning , {a.Name} destroyed {b.Name} is alive");
                return (true, true);
                }
        if (InstaKillCB)
                {
                b.HP = 0;
                Console.WriteLine($"{c.Name} winning , {b.Name} destroyed {a.Name} is alive");
                return (true, true);
                }
        if (InstaKillBC)
                {
                c.HP = 0;
                Console.WriteLine($"{b.Name} winning , {c.Name} destroyed {a.Name} is alive");
                return (true, true);
                }
        if (InstaKillBA)
                {
                a.HP = 0;
                Console.WriteLine($"{b.Name} winning , {a.Name} destroyed {c.Name} is alive");
                return (true, true);
                }
               var AvialableShieldAC = c.Shield - a.Damage;
               var AvialableShieldAB = b.Shield - a.Damage;
               var AvialableShieldCB = b.Shield - c.Damage;
               var AvialableShieldCA = a.Shield - c.Damage;
               var AvialableShieldBA = a.Shield - b.Damage;
               var AvialableShieldBC = b.Shield - c.Damage;
        if (AvialableShieldCB >= 0)
                {
                b.Shield = AvialableShieldCB;
                return (true, true);
                }
        if (AvialableShieldCA >= 0)
                {
                a.Shield = AvialableShieldCA;
                return (true, true);
                }
        if (AvialableShieldAB >= 0)
                {
                b.Shield = AvialableShieldAB;
                return (true, true);
                }
        if (AvialableShieldAC >= 0)
                {
                c.Shield = AvialableShieldAC;
                return (true, true);
                }
        if (AvialableShieldBC >= 0)
                {  
                c.Shield = AvialableShieldBC;
                return (true, true);
                }
        if (AvialableShieldBA >= 0)
                {
                a.Shield = AvialableShieldBA;
                return (true, true);
                }
                 var AdditionalHpDamageAC = 0 - AvialableShieldAC;
                 var AdditionalHpDamageAB = 0 - AvialableShieldAB;
                 var AdditionalHpDamageBC = 0 - AvialableShieldBC;
                 var AdditionalHpDamageBA = 0 - AvialableShieldBA;
                 var AdditionalHpDamageCA = 0 - AvialableShieldCA;
                 var AdditionalHpDamageCB = 0 - AvialableShieldCB;
                 var HpAvialableAC = c.HP - AdditionalHpDamageAC;
                 var HpAvialableAB = b.HP - AdditionalHpDamageAB;
                 var HpAvialableCB = b.HP - AdditionalHpDamageCB;
                 var HpAvialableCA = a.HP - AdditionalHpDamageCA;
                 var HpAvialableBA = a.HP - AdditionalHpDamageBA;
                 var HpAvialableBC = c.HP - AdditionalHpDamageBC;
       if       (HpAvialableAC <= 0)
               {
                c.HP = 0;  
                Console.WriteLine($"{a.Name} winning, {c.Name} destroyed {b.Name}  still alive");
             return (true, true);
               }
       if      (HpAvialableAB <= 0)
               {
                b.HP = 0;
                Console.WriteLine($"{a.Name} winning, {b.Name} destroyed, {c.Name} still alive");
             return (true, true);
                }
       if     (HpAvialableBA <= 0)
                {
                a.HP = 0;
                Console.WriteLine($"{b.Name} winning, {a.Name} destroyed, {c.Name} still alive");
             return (true, true);
                }
       if    (HpAvialableBC <= 0)
                {
                c.HP = 0;
                Console.WriteLine($"{b.Name} winning, {c.Name} destroyed, {b.Name} still alive");
             return (true, true);
                }
       if     (HpAvialableCA <= 0)
               { 
                a.HP = 0;
                Console.WriteLine($"{c.Name} winning, {a.Name} destroyed, {b.Name} still alive");
             return (true, true);
                }
       if     (HpAvialableCB <= 0)
               {
                b.HP = 0;
                Console.WriteLine($"{c.Name} winning, {b.Name} destroyed, {a.Name} still alive");
             return (true, true);
                }
            var FinalDamageOverA = (AdditionalHpDamageCA + AdditionalHpDamageBA);
            var FinalDamageOverB = (AdditionalHpDamageAB + AdditionalHpDamageCB);
            var FinalDamageOverC = (AdditionalHpDamageBC + AdditionalHpDamageAC);
            b.HP = (b.HP + b.Shield) - FinalDamageOverB;
            c.HP = (c.HP + c.Shield) - FinalDamageOverC;
            a.HP =  (a.HP + a.Shield) - FinalDamageOverA;
             return (true, true);
        }

        public void Fight()
        {
                  var random = new Random();
                  var begin = random.Next(0, 3);
                  var FirstAttack1 = begin == 0;
                  var FirstAttack2 = begin == 1;
                  var FirstAttack3 = begin == 2;
                  var AttackOrder1 = FirstAttack1;
                  var AttackOrder2 = FirstAttack2;
                  var AttackOrder3 = FirstAttack3;
                  var FirstAttackHappend = false;
            while (_first.HP != 0 && _third.HP != 0 && _second.HP != 0)
            {
            if          (AttackOrder1)
                       {
                          var (vision, fight) = AttackTick(_first, _second, _third, FirstAttackHappend);
                          _second.Ultimate(_first);
                          _third.Ultimate(_second);
                          FirstAttackHappend = vision;
            if          (!vision)
                       {
                        _first.Move();
                        _second.Move();
                        _third.Move();
                        Console.WriteLine($"Ships are moving in the case of {_first.Name} Attack !");
                       }
                        Console.WriteLine($"{_first.Name}, attacked {_second.Name} and {_third.Name},{_second.Name} HP left {_second.HP}, Shield left {_second.Shield}, {_third.Name} HP left {_third.HP}, Shield left {_third.Shield}");
                       }
            if         (AttackOrder2)
                       {
                         var (vision, fight) = AttackTick(_second, _first, _third, FirstAttackHappend);
                        _first.Ultimate(_second);
                        _third.Ultimate(_second);
                        FirstAttackHappend = vision;
            if          (!vision)
                      {
                        _first.Move();
                        _second.Move();
                        _third.Move();
                        Console.WriteLine($"Ships are moving in the case of {_second.Name} Attack !");
                      }
                        Console.WriteLine($"{_second.Name}, attacked {_first.Name} and {_third.Name}, {_first.Name} HP left {_first.HP}, Shield left {_first.Shield},{_third.Name} HP left {_third.HP}, Shield left {_third.Shield}");
                      }
            if         (AttackOrder3)
                      {

                    var (vision, fight) = AttackTick(_third, _second, _first, FirstAttackHappend);
                    _second.Ultimate(_third);
                    _first.Ultimate(_third);
                    FirstAttackHappend = vision;
            if     (!vision)
                       {
                        _first.Move();
                        _second.Move();
                        _third.Move();
                        Console.WriteLine($"Ships are moving in the case of {_third.Name} Attack !");
                        }
                    Console.WriteLine($"{_third.Name}, attacked {_second.Name} and {_first.Name},{_second.Name} HP left {_second.HP}, Shield left {_second.Shield}, {_first.Name} HP left {_first.HP}, Shield left {_first.Shield}");
                }
                AttackOrder1 = !AttackOrder1;
                AttackOrder2 = !AttackOrder2;
                AttackOrder3 = !AttackOrder3;
                Console.WriteLine($"Ended_Fight_Log: {_second.Name} HP left {_second.HP} Shield left {_second.Shield}, {_first.Name} HP left {_first.HP}, Shield left {_first.Shield}, {_third.Name} HP left {_third.HP}, Shield left {_third.Shield}");
            }
        }
    }
}

public class BaseShip
{
    public void Move()
    {
        VisionRange += Speed;
        VisibilityRange -= Speed;
    }
    public virtual void Ultimate(BaseShip target)
    {

    }
    public virtual void IncomeDamage(int Damage)
    {

    }
    public string Name { get; set; }
    public int Speed { get; set; }
    public int Damage { get; set; }
    public int HP {  get; set; }
    public int VisionRange { get; set; }
    public int VisibilityRange { get; set; }
    public int Shield {  get; set; }
}
public class Критовик : BaseShip
{
    public Критовик()
    {
        Name = "Критовик";
        Speed = 15;
        Damage = 15;
        HP = 500;
        Shield = 750;
        VisionRange = 200;
        VisibilityRange = 100;
    }
}
public class Танк : BaseShip
{
    public Танк()
    {
        Name = "Танк";
        Speed= 15;
        Damage = 10;
        HP = 500;
        Shield = 1500;
        VisionRange = 100;
        VisibilityRange = 200;
    }
}
public class Ловкач : BaseShip
{
    public Ловкач()
    {
        Name = "Ловкач";
        Speed = 25;
        Damage = 20;
        HP = 500;
        Shield= 400;
        VisionRange = 250;
        VisibilityRange = 50;

    }
}