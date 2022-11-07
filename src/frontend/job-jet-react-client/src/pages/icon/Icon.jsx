import * as DiIcons from "react-icons/di";
import * as BiIcons from "react-icons/bi";
import * as SiIcons from "react-icons/si";
import * as MdIcons from "react-icons/md";
import * as RiIcons from "react-icons/ri";
import * as BsIcons from "react-icons/bs";
import * as IoIcons from "react-icons/io5";

import * as TiIcons from "react-icons/ti";

const nameMapping = {
  Di: DiIcons,
  Bi: BiIcons,
  Si: SiIcons,
  Md: MdIcons,
  Ri: RiIcons,
  Bs: BsIcons,
  Io: IoIcons,
  Ti: TiIcons,
};

export const Icon = (props) => {
  const { iconName, size, color, className } = props;

  const Icon = nameMapping[iconName.slice(0, 2)][iconName];

  return (
    <span className={className} style={{ fontSize: size, color: color }}>
      <Icon />
    </span>
  );
};
