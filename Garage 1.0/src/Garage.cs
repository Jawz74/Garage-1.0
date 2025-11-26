using System;
using System.Collections;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Garage_1._0.src
{

    // Det ska gå att iterera över en instans av Garage med foreach, därför måste klassen implementera IEnumerable<T> som foreach kräver.
    // IEnumerable<T> specar metoden GetEnumerator(), som exponerar en iterator som foreach behöver, den måste därför implementeras här.
    // Implementationen av GetEnumerator() nedan loopar igenom arrayen och returnerar varje T-objekt (Vehicle), ett i taget med en 'yield return',
    // vilket automatiskt genererar en intern, anonym "iterator"-klass av typen enumerator. (Man slipper därmed implementera MoveNext(), Current och Reset manuellt.)
    //
    // Eftersom IEnumerable<T> ärver från den äldre IEnumerable måste även den icke-generiska versionen GetEnumerator() implementeras.
    // Denna version anropar bara den generiska versionen och finns specad för bakåtkompatibilitet.

    public class Garage<T> : IEnumerable<T> where T : Vehicle // Den generiska typen T begränsas här till Vehicle (med where).
    {
        // Fält
        private readonly T?[] _vehicles;   // Intern lagring av Vehichles i en array, som även kan innehålla null = tomma parkeringsplatser.

        //Properties
        public bool IsFull
        {
            get
            {
                return _vehicles.All(v => v != null);
            }
        }
        public bool IsEmpty
        {
            get
            {
                return _vehicles.All(v => v == null);
            }
        }
        public int Count
        {
            get
            {
                return _vehicles.Count(v => v != null);
            }
        }

        public int Capacity
        {
            get
            {
                return _vehicles.Length;
            }
        }

        // Konstruktor
        public Garage(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity måste vara större än noll!.", nameof(capacity)); // Ett garage måste ha minst 1 plats 

            _vehicles = new T?[capacity];  // Skapar upp den interna Vehicle-arrayen, med capacity antal platser                                          
        }

        // Interfacet IEnumerable<T> kräver metoden GetEnumerator() där en Enumerator<T> returneras, via returtypen IEnumerator<T>, (T är här Vehicle) 
        public IEnumerator<T> GetEnumerator()
        {
            // Returnerar endast de fordon som finns i garaget, alltså EJ tomma parkeringsplatser (dvs null)
            foreach (var v in _vehicles)
                if (v != null)
                    yield return v;
        }

        // Detta är den icke-generiska versionen av GetEnumerator(), dvs returnerar endast en IEnumerator (utan <T>)
        // Explicit namngivning: GetEnumerator() finns på IEnumerable och inte på IEnumerable<T> och ska implementeras. 
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ParkResult Park(T objectToPark)  // Alla Vehicles (T) har ett reg.nr - görs konstruktorn.
        {
            if (!IsFull) // Om lediga p-platser finns i garaget
            {

                // Det finns redan ett fordon med aktuellt reg.nr i garaget
                if (FindVehicleByRegNumber(objectToPark.RegistrationNumber) != null)  // Todo: Ev. lägga denna funktionalitet i loopen nedan istället?
                    return ParkResult.AlreadyExists;

                for (int i = 0; i < _vehicles.Length; i++)
                {
                    if (_vehicles[i] == null)    // Lägg in objectToPark på första lediga plats (null) i _vehicles 
                    {
                        _vehicles[i] = objectToPark;
                        return ParkResult.Success;
                    }
                }
            }

            return ParkResult.GarageFull;
        }

        public UnparkResult RemoveByRegNumber(string regNumber) // Todo: Om Vehicle med registrationNumber saknas - bara returnera false, kasta ett fel eller kanske både returnera false och en out string message?
        {
            if (!IsEmpty) // Om det finns fordon i garaget
            {
                for (int i = 0; i < _vehicles.Length; i++)
                {
                    if (_vehicles[i]?.RegistrationNumber == regNumber)            // Ta bort vehicle med .regNumber ur _vehicles 
                    {
                        _vehicles[i] = null;
                        return UnparkResult.Success;
                    }
                }

                return UnparkResult.RegNoNotFound;
            }

            return UnparkResult.GarageEmpty;
        }

        public T? FindVehicleByRegNumber(string regNumber)
        {

            for (int i = 0; i < _vehicles.Length; i++)
            {
                if (_vehicles[i]?.RegistrationNumber == regNumber)
                {
                    return _vehicles[i];
                }
            }

            return null; // Fordon med regNumber saknas och/eller garaget är tomt
        }

    }

}