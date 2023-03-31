using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CreatorFirstPersonCamera: CreatorCamera
{
        // Обратите внимание, что сигнатура метода по-прежнему использует тип
        // абстрактного продукта, хотя  фактически из метода возвращается
        // конкретный продукт. Таким образом, Создатель может оставаться
        // независимым от конкретных классов продуктов.
        public override ICameraOperation FactoryMethod(GameObject car)
        {
            FirstPersonCamera camera = car.AddComponent<FirstPersonCamera>();
            return camera;
        }
}

