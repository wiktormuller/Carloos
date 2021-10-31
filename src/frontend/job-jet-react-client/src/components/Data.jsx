import '../styles/main-styles.css';
import { InputData } from "./InputData";
import { FilterData } from "./FilterData";
import { AdvertData } from './AdvertData';



export const Data = () => {
    return (
      <div className="data">
        <InputData></InputData>
        <FilterData></FilterData>
        <AdvertData></AdvertData>
      </div>
      );
}