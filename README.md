# UnityDataObject
Data object container for unity, allow for decoupling data and components that need it.
The goal is to allow components to dynamically bind and re-bind during runtime to any data provided.

# Usage case
Let's have a data class EnemyData containing informations about an enemy and a prefab with component HealthBar displaying health of the enemy.
We want to have HealthBar decoupled from the EnemyData and allow for dynamic binding to different EnemyData during runtime.
This is achieved with following setup.

Data class 
```C#
public class EnemyData {
	public string Name;

	public int HealthValue;
}
```

Enemy data data object
```C#
public class EnemyDataObject : DataObject<EnemyData> { }
```

Health bar
```C#
public class HealthBar : MonoBehaviour {
	[SerializeField]
	EnemyDataObject _Data;

	protected void Awake() {
		_Data.Subscribe((EnemyData oldValue, EnemyData newValue) => {
			if(oldValue != null) {
				// perform cleanup
			}

			if (newValue == null) 
				return;

			// perform initialisation
		});
	}
}
```

Now we create prefab with HealthBar and EnemyDataObject component and link that EnemyDataObjet to HealthBar.
Then during runtime each time when EnemyData on EnemyDataObject changes HealthBar is initialised / de-initialised accordingly.