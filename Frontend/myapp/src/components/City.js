import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPenToSquare } from "@fortawesome/free-solid-svg-icons";
import { faTrash } from "@fortawesome/free-solid-svg-icons";

export const City = ({ cities, deleteCity, editCity }) => {
  return (
    <div className="City">
      <ul>
        {cities.map((city) => (
          <li key={city.id}>
            City Name: {city.cityName}, Country: {city.country}, Population:{" "}
            {city.population} <p></p>
            <FontAwesomeIcon
              className="edit-icon"
              icon={faPenToSquare}
              onClick={() => editCity(city)}
            />
            <FontAwesomeIcon
              className="delete-icon"
              icon={faTrash}
              onClick={() => deleteCity(city.id)}
            />
          </li>
        ))}
      </ul>
    </div>
  );
};

export default City;
