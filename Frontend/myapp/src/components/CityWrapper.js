import React, { useState } from "react";
import CityForm from "./CityForm";
import { v4 as uuidv4 } from "uuid";
import City from "./City";
import EditCityForm from "./EditCityForm";

export const CityWrapper = () => {
  const [cities, setCities] = useState([]);
  const addCity = (city) => {
    setCities((cities) => [
      ...cities,
      { id: uuidv4(), ...city, completed: false, isEditing: false },
    ]);
    console.log(cities);
  };

  const deleteCity = (id) => {
    setCities(cities.filter((city) => city.id !== id));
  };

  const editCity = (editedCity) => {
    setCities((cities) =>
      cities.map((city) =>
        city.id === editedCity.id ? { ...editedCity, isEditing: false } : city
      )
    );
  };
  return (
    <div className="CityWrapper">
      <h1>Add a new city!</h1>
      <CityForm addCity={addCity} />

      {cities.length === 0 ? (
        <div>No cities added yet.</div>
      ) : (
        cities.map((city) =>
          city.isEditing === city.id ? (
            <EditCityForm key={city.id} city={city} editCity={editCity} />
          ) : (
            <City
              key={city.id}
              city={city}
              deleteCity={deleteCity}
              editCity={() => editCity(city.id)}
            />
          )
        )
      )}
    </div>
  );
};

export default CityWrapper;
