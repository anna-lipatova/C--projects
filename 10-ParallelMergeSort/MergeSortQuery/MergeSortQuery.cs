using LibraryModel;
using LibraryQueryContracts;

// NOTE: Run your solution with at least the "Case 1" launch setting. If you have enough physical memory, you should try "Case 2" and ideally "Case 3" and "Case 4" as well.
// NOTE: Run in "Release" configuration! Debug in "Debug" configuration.
// IMPORTANT NOTE: Verify, that all files ResultMergesort????Threads.txt generated for your solution have the same content as ResultReferenceParallel.txt and ResultReferenceSingleThreaded.txt !!!

// IMPORTANT NOTE: Put your solution into this project only!
//				   You can add any code into the MergeSortQuery.MergeSortQuery class, and its ExecuteQuery method.
//				   The MergeSortQuery.MergeSortQuery class has to implement the IParallelQuery interface.
//				   You can also add any additional nested types into MergeSortQuery.MergeSortQuery class if necessary.
//				   You can also add any additional types into this file and project if necessary.

namespace MergeSortQuery {
	public class MergeSortQuery : IParallelQuery {
		public Library? Library { get; set; }
		public int ThreadCount { get; set; }

		public List<Copy> ExecuteQuery() {
			if (Library is null) throw new InvalidOperationException($"{nameof(Library)} property not set and default null value is not valid.");
			if (ThreadCount == 0) throw new InvalidOperationException($"{nameof(ThreadCount)} property not set and default value 0 is not valid.");

			return new List<Copy>();
		}
	}
}
