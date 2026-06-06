import { useQuery } from "@tanstack/react-query";
import { fetchSkills } from "@/lib/api";

export function useSkills() {
  return useQuery({
    queryKey: ["skills"],
    queryFn: fetchSkills,
    staleTime: 5 * 60 * 1000,
  });
}
