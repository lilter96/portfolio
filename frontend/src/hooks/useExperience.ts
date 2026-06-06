import { useQuery } from "@tanstack/react-query";
import { fetchExperience } from "@/lib/api";

export function useExperience() {
  return useQuery({
    queryKey: ["experience"],
    queryFn: fetchExperience,
    staleTime: 5 * 60 * 1000,
  });
}
