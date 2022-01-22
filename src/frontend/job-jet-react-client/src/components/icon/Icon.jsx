import * as DiIcons from "react-icons/di";
import * as BiIcons from "react-icons/bi";
import * as SiIcons from "react-icons/si";
import * as MdIcons from "react-icons/md";
import * as RiIcons from "react-icons/ri";
import * as BsIcons from "react-icons/bs";

const nameMapping = {
  Di: DiIcons,
  Bi: BiIcons,
  Si: SiIcons,
  Md: MdIcons,
  Ri: RiIcons,
  Bs: BsIcons,
};

export const Icon = (props) => {
  const { iconName, size, color } = props;

  const Icon = nameMapping[iconName.slice(0, 2)][iconName];

  return (
    <div style={{ fontSize: size, color: color }}>
      <Icon />
    </div>
  );
};
