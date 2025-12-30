interface PaginationProps {
  pageNumber: number;
  totalPage: number;
  totalRecords: number;
  onPageChange: (page: number) => void;
}

const Pagination = ({
  pageNumber,
  totalPage,
  totalRecords,
  onPageChange,
}: PaginationProps) => {
  if (totalPage <= 1) return <></>;

  return (
    <div className="flex items-center justify-between mt-6">
      {/* Info */}
      <p className="text-sm text-gray-600">
        Showing page <b>{pageNumber}</b> of <b>{totalPage}</b> ({totalRecords}{" "}
        records)
      </p>

      {/* Controls */}
      <div className="flex gap-2">
        <button
          disabled={pageNumber === 1}
          onClick={() => onPageChange(pageNumber - 1)}
          className="px-3 py-1 rounded bg-gray-200 disabled:opacity-50"
        >
          Prev
        </button>

        {Array.from({ length: totalPage }, (_, i) => i + 1).map((page) => (
          <button
            key={page}
            onClick={() => onPageChange(page)}
            className={`px-3 py-1 rounded ${
              page === pageNumber ? "!bg-blue-600 text-white" : "!bg-gray-200"
            }`}
          >
            {page}
          </button>
        ))}

        <button
          disabled={pageNumber === totalPage}
          onClick={() => onPageChange(pageNumber + 1)}
          className="px-3 py-1 rounded !bg-gray-200 disabled:opacity-50"
        >
          Next
        </button>
      </div>
    </div>
  );
};

export default Pagination;
