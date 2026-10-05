using System;

namespace Assignment03_Part1
{
    // ================= Q9: BEFORE interfaces =================
    // Problem: forcing every vehicle to implement all 4 methods makes Car/Ship carry
    // MoveUp()/MoveDown() they can't support (empty or exception-throwing methods).
    // The contract lies about what the class can do. Interfaces fix this.
    public class CarBefore
    {
        public void MoveForward() => Console.WriteLine("Car moves forward.");
        public void MoveBackward() => Console.WriteLine("Car moves backward.");
        public void MoveUp() => throw new NotSupportedException("A car can't move up!");
        public void MoveDown() => throw new NotSupportedException("A car can't move down!");
    }

    // ================= Q10: interfaces =================
    public interface IMoveable
    {
        void MoveForward();
        void MoveBackward();
    }

    public interface IFlyable
    {
        void MoveUp();
        void MoveDown();
    }

    // ================= Q11: implementations =================
    public class Car : IMoveable
    {
        public void MoveForward() => Console.WriteLine("Car is moving forward on the road.");
        public void MoveBackward() => Console.WriteLine("Car is moving backward on the road.");
    }

    public class Ship : IMoveable
    {
        public void MoveForward() => Console.WriteLine("Ship is sailing forward on the sea.");
        public void MoveBackward() => Console.WriteLine("Ship is sailing backward on the sea.");
    }

    public class Airplane : IMoveable, IFlyable
    {
        public void MoveForward() => Console.WriteLine("Airplane is flying forward in the air.");
        public void MoveBackward() => Console.WriteLine("Airplane is moving backward in the air.");
        public void MoveUp() => Console.WriteLine("Airplane is climbing up in the air.");
        public void MoveDown() => Console.WriteLine("Airplane is descending down in the air.");
    }

    // ================= Q13: IVehicle + Vehicle =================
    // Benefit: groups related contracts into one, so a class implements a single
    // interface to get the whole set, and code can depend on one type.
    public interface IVehicle : IMoveable, IFlyable
    {
    }

    public class Vehicle : IVehicle
    {
        public virtual void MoveForward() => Console.WriteLine("Vehicle moves forward.");
        public virtual void MoveBackward() => Console.WriteLine("Vehicle moves backward.");
        public virtual void MoveUp() => Console.WriteLine("Vehicle moves up.");
        public virtual void MoveDown() => Console.WriteLine("Vehicle moves down.");
    }

    // ================= Q14: explicit implementation =================
    public class ShipExplicit : IMoveable
    {
        // Explicit: no access modifier, only reachable through an IMoveable reference.
        void IMoveable.MoveForward() => Console.WriteLine("Ship (explicit) is sailing forward on the sea.");

        public void MoveBackward() => Console.WriteLine("Ship (explicit) is sailing backward on the sea.");
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // ---------- Q12: concrete types ----------
            Console.WriteLine("=== Q12: concrete types ===");
            Car car = new Car();
            car.MoveForward();
            car.MoveBackward();

            Ship ship = new Ship();
            ship.MoveForward();
            ship.MoveBackward();

            Airplane airplane = new Airplane();
            airplane.MoveForward();
            airplane.MoveBackward();
            airplane.MoveUp();
            airplane.MoveDown();

            // ---------- Q12: interface references ----------
            Console.WriteLine("\n=== Q12: interface references ===");
            IMoveable carRef = new Car();
            IMoveable planeRef = new Airplane();
            carRef.MoveForward();
            carRef.MoveBackward();
            planeRef.MoveForward();
            planeRef.MoveBackward();

            // planeRef.MoveUp();  // COMPILE ERROR: IMoveable has no MoveUp().
            // The compiler checks the reference type, not the object type.
            // Need an IFlyable (or Airplane) reference:
            IFlyable flyRef = (IFlyable)planeRef;
            flyRef.MoveUp();
            flyRef.MoveDown();

            // ---------- Q13 ----------
            Console.WriteLine("\n=== Q13: IVehicle / Vehicle ===");
            IVehicle vehicle = new Vehicle();
            vehicle.MoveForward();
            vehicle.MoveBackward();
            vehicle.MoveUp();
            vehicle.MoveDown();

            // ---------- Q14 ----------
            Console.WriteLine("\n=== Q14: explicit implementation ===");
            ShipExplicit s = new ShipExplicit();
            // s.MoveForward();  // COMPILE ERROR CS1061: not visible on the class reference
            s.MoveBackward();
            ((IMoveable)s).MoveForward();   // call through the interface
            IMoveable m = s;
            m.MoveForward();

            Console.ReadKey();
        }
    }
}
