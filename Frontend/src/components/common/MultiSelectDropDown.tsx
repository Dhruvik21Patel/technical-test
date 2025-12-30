import { useState, useRef, useEffect } from "react";

interface Option {
  id: number;
  name: string;
}

interface Props {
  options: Option[];
  value: number[];
  onChange: (value: number[]) => void;
}

const MultiSelectDropdown = ({ options, value, onChange }: Props) => {
  const [open, setOpen] = useState(false);
  const dropdownRef = useRef<HTMLDivElement>(null);

  // Close when clicking outside
  useEffect(() => {
    const handler = (e: MouseEvent) => {
      if (
        dropdownRef.current &&
        !dropdownRef.current.contains(e.target as Node)
      ) {
        setOpen(false);
      }
    };
    document.addEventListener("mousedown", handler);
    return () => document.removeEventListener("mousedown", handler);
  }, []);

  const toggleOption = (id: number) => {
    if (value.includes(id)) {
      onChange(value.filter((v) => v !== id));
    } else {
      onChange([...value, id]);
    }
  };

  return (
    <div className="relative" ref={dropdownRef}>
      <div
        onClick={() => setOpen(!open)}
        className="border rounded px-3 py-2 cursor-pointer bg-white flex flex-wrap gap-1"
      >
        {value.length === 0 ? (
          <span className="text-gray-400">Select categories</span>
        ) : (
          value.map((id) => {
            const item = options.find((o) => o.id === id);
            return (
              <span
                key={id}
                className="bg-blue-100 text-blue-700 px-2 py-1 rounded text-sm"
              >
                {item?.name}
              </span>
            );
          })
        )}
      </div>
      {open && (
        <div className="absolute z-10 w-full bg-white border mt-1 rounded shadow max-h-52 overflow-auto">
          {options.map((opt) => (
            <label
              key={opt.id}
              className="flex items-center gap-2 px-3 py-2 hover:bg-gray-100 cursor-pointer"
            >
              <input
                type="checkbox"
                checked={value.includes(opt.id)}
                onChange={() => toggleOption(opt.id)}
              />
              {opt.name}
            </label>
          ))}
        </div>
      )}
    </div>
  );
};

export default MultiSelectDropdown;
